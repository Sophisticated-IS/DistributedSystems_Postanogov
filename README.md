# Реализация клиент серверной архитектры ч/з CommunicationModule и MessagesDispatcher
- [x] Sockets CommunicationModule
- [x] Kafka CommunicationModule
- [x] GRPC CommunicationModule (GRPC + TLS through self signed *.pfx certificate with old implementation of GRPC - GRPC.Core, NOT NEW ASP Net Core implementation of GRPC)

# Задание 
На этапе проектирования системы необходимо выбрать предметную область и спроектировать нормализованную и ненормализованную базу данных (БД). Ненормализованная БД должна храниться в формате SQLite, Microsoft Access, DBF или другом формате настольной (файловой) БД и иметь одну таблицу. Нормализованная БД должна храниться в корпоративной СУБД – PostgreSQL, Microsoft SQL Server, Oracle или MySQL – и иметь минимум пять таблиц.  

Сервис обмена данными должен выполнять прием данных в нормализованную БД. Непосредственно процесс нормализации может реализовывать либо программа экспорта, либо программа импорта (по выбору студента).
