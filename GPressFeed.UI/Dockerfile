FROM node:18-alpine

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . . 

RUN npm run build

USER node:node

CMD ["node","build/index.js"]