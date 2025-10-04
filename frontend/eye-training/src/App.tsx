import React, { useState } from "react";

interface UserVision {
  visionLeftEye: number;
  visionRightEye: number;
  cylinderLeftEye: number;
  cylinderRightEye: number;
  creationDate: string;
}

interface User {
  id: number;
  firstName: string;
  lastName: string;
  userName: string;
  visions: UserVision[];
}

const App: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const fetchUsers = async () => {
  try {
    const res = await fetch("http://localhost:5225/api/users");
    if (!res.ok) throw new Error("Failed to fetch users");

    const data = await res.json();
    console.log("Fetched users:", data);

    setUsers(data);
  } catch (err) {
    console.error(err);
  }
};
  return (
    <div style={{ padding: "20px" }}>
      <h1>Users</h1>
      <button onClick={fetchUsers}>Prikaži korisnike</button>
      <ul>
        {users.map((u) => (
          <li key={u.id} style={{ marginTop: "10px" }}>
            <strong>{u.firstName} {u.lastName}</strong> ({u.userName})
            <ul>
              {u.visions.map((v, idx) => (
                <li key={idx}>
                  L: {v.visionLeftEye} ({v.cylinderLeftEye}), R: {v.visionRightEye} ({v.cylinderRightEye}) - {new Date(v.creationDate).toLocaleDateString()}
                </li>
              ))}
            </ul>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default App;
