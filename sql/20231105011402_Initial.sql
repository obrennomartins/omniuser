CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory`
(
    `MigrationId`    varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4  NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET = utf8mb4;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231105011402_Initial') THEN
        ALTER DATABASE CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231105011402_Initial') THEN
        CREATE TABLE `RegistrosAuditoria`
        (
            `Id`           int                        NOT NULL AUTO_INCREMENT,
            `Usuario`      text CHARACTER SET utf8mb4 NULL,
            `Entidade`     text CHARACTER SET utf8mb4 NOT NULL,
            `Acao`         text CHARACTER SET utf8mb4 NOT NULL,
            `Timestamp`    timestamp                  NOT NULL,
            `Alteracoes`   text CHARACTER SET utf8mb4 NOT NULL,
            `CriadoEm`     datetime(6)                NOT NULL,
            `AtualizadoEm` datetime(6)                NOT NULL,
            CONSTRAINT `PK_RegistrosAuditoria` PRIMARY KEY (`Id`)
        ) CHARACTER SET = utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231105011402_Initial') THEN
        CREATE TABLE `Usuarios`
        (
            `Id`           int                        NOT NULL AUTO_INCREMENT,
            `Nome`         text CHARACTER SET utf8mb4 NOT NULL,
            `Email`        text CHARACTER SET utf8mb4 NULL,
            `Telefone`     text CHARACTER SET utf8mb4 NULL,
            `Documento`    text CHARACTER SET utf8mb4 NULL,
            `Ativo`        boolean                    NOT NULL,
            `CriadoEm`     timestamp                  NOT NULL,
            `AtualizadoEm` timestamp                  NOT NULL,
            CONSTRAINT `PK_Usuarios` PRIMARY KEY (`Id`)
        ) CHARACTER SET = utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231105011402_Initial') THEN
        CREATE TABLE `Endereco`
        (
            `Id`           int                        NOT NULL AUTO_INCREMENT,
            `UsuarioId`    int                        NOT NULL,
            `Logradouro`   text CHARACTER SET utf8mb4 NOT NULL,
            `Numero`       text CHARACTER SET utf8mb4 NOT NULL,
            `Complemento`  text CHARACTER SET utf8mb4 NULL,
            `Cep`          text CHARACTER SET utf8mb4 NOT NULL,
            `Bairro`       text CHARACTER SET utf8mb4 NOT NULL,
            `Cidade`       text CHARACTER SET utf8mb4 NOT NULL,
            `Uf`           text CHARACTER SET utf8mb4 NOT NULL,
            `CriadoEm`     timestamp                  NOT NULL,
            `AtualizadoEm` timestamp                  NOT NULL,
            CONSTRAINT `PK_Endereco` PRIMARY KEY (`Id`),
            CONSTRAINT `FK_Endereco_Usuarios_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuarios` (`Id`) ON DELETE CASCADE
        ) CHARACTER SET = utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231105011402_Initial') THEN
        CREATE UNIQUE INDEX `IX_Endereco_UsuarioId` ON `Endereco` (`UsuarioId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231105011402_Initial') THEN

        INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
        VALUES ('20231105011402_Initial', '7.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

