FROM mysql:5.7.22
EXPOSE 3306
COPY ./RestWithAspNetAPI/db/migrations/ /home/database/
COPY ./RestWithAspNetAPI/db/dataset/ /home/database/
COPY ./RestWithAspNetAPI/ci/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh