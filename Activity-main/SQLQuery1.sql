use projectdb

SELECT * FROM MK_USERS

SELECT * FROM MK_UserConnectionLogs

SELECT * FROM MK_Roles

drop table mk_users,MK_Roles,MK_UserConnectionLogs,MK_Winners,MK_Participations,MK_Activities,MK_Categories,MK_Cities;

DELETE FROM MK_Roles

alter table MK_Users nocheck constraint all

alter table MK_Roles nocheck constraint all