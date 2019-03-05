FROM gcr.io/aje-coop/mean:1
ADD . /app
WORKDIR /app/tracker-ui
RUN npm install
RUN ng build --prod --base-href /
RUN mv dist/tracker-ui /app/tracker-service/src/dist
WORKDIR /app/tracker-service
RUN npm install
CMD mongod & sleep 5 && ts-node src/index.ts