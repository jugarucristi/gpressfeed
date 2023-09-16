FROM node:18-alpine

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . . 

RUN npm run build

EXPOSE 80

USER node:node

CMD ["node","build/index.js"]