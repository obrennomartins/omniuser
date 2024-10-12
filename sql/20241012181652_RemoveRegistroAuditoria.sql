START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20241012181652_RemoveRegistroAuditoria') THEN
    DROP TABLE "RegistrosAuditoria";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20241012181652_RemoveRegistroAuditoria') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20241012181652_RemoveRegistroAuditoria', '7.0.13');
    END IF;
END $EF$;
COMMIT;

