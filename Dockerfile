FROM gcr.io/aje-coop/mean:2
ADD . /app

WORKDIR /app/tracker-ui
RUN npm install
RUN ng build --prod --base-href /
RUN mv dist/tracker-ui /app/tracker-service/src/dist

WORKDIR /app/tracker-service
RUN npm install

ADD ./db/crontab /etc/cron.d/mongo-cron
RUN chmod 0644 /etc/cron.d/mongo-cron
RUN crontab /etc/cron.d/mongo-cron

CMD mongod & sleep 5 && cron & ts-node src/index.ts