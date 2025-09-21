FROM node:20 AS build
WORKDIR /app

COPY eye-training/package*.json ./
RUN npm install --legacy-peer-deps

WORKDIR /app

COPY eye-training ./

RUN npm run build

FROM node:20-slim AS runtime
WORKDIR /app

RUN npm install -g serve
COPY --from=build /app/dist ./dist
EXPOSE 3000
CMD ["serve", "-s", "dist", "-l", "3000"]
