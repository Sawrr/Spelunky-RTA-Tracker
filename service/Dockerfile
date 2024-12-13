FROM node:slim

ADD . /app

WORKDIR /app
RUN npm install --legacy-peer-deps

CMD npx ts-node src/index.ts
