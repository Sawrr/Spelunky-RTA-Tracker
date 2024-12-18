FROM mongo:latest

RUN apt-get update && apt-get -y install cron

ADD . /db
WORKDIR /db

ADD ./crontab /etc/cron.d/mongo-cron
RUN chmod 0644 /etc/cron.d/mongo-cron
RUN crontab /etc/cron.d/mongo-cron

CMD mongod --bind_ip_all & cron & sleep infinity
