export interface UserVision {
  visionLeftEye: number;
  visionRightEye: number;
  cylinderLeftEye: number;
  cylinderRightEye: number;
  creationDate: string;
}

export interface User {
  id: number;
  firstName: string;
  lastName: string;
  userName: string;
  visions: UserVision[];
}
