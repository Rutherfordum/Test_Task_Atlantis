# Тестовое задание на позицию Unity Developer в компанию Atlantis
[Ознакомиться с тестовым заданием](https://github.com/Rutherfordum/Test_Task_Atlantis/blob/main/Resources/TestTaskAtlantis.md)

![Alt Text](https://github.com/Rutherfordum/Test_task_1C_Publishing/blob/main/Resources/Movie_001.gif)

## Описание проекта  
`UserInputService` - сервис отвечает за считывание пользовательского ввода, WASD на основе новой системы ввода в Unity new InputSystem  
`ScreenSizeService` - отвечает за границы экрана, добавлен в конфиг смещение границ экрана для удобства настройки  
`PlayerMoveService` - отвечает за движение игрока  
`AutoShootService` - отвечает за автоматическую стрельбу  
`EnemySpawnerService` - отвечает за появление врагов, появление реализовано через абстрактную фабрику, если вдруг надо будет использовать группу обьектов, причем кол-во врагов спавниться сразу все, потом просто вклюаем с переодичностью указаной в конфиге  
`PlayerDistanceWatcherEnemyService` - отвечает за дальность определения врагов, если враг в зоне видимости то стреляем и наоборот  
`EnemyWatcherService` - сервис отвечает за слежением за всех врагов на сцене, чтобы быть уверенным что всех врагов мы уничтожили  

Все конфигурационные файлы для игры находяться по пути `Assets/_Project/Resources`  
Все префабы врагов так же находяться по пути `Assets/_Project/Resources/Enemies`  
Все префабы пуль находяться `Assets/_Project/Resources/Bullets`

Рекомендую выбрать в окне `Game` режим `Simulator`

## Инструменты используемые в проекте
[com.svermeulen.extenject](https://github.com/Mathijs-Bakker/Extenject.git?path=UnityProject/Assets/Plugins/Zenject/Source#9.3.0) 

[com.cysharp.unitask](https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask) 

[dotween.demigiant](https://dotween.demigiant.com/)

## Ассеты используемы в проекте
[Simple 2D Platformer Assets Pack](https://assetstore.unity.com/packages/2d/characters/simple-2d-platformer-assets-pack-188518)

## Мои контакты
[Telegram](https://t.me/Rutherfordum)   
[TgChanel](https://t.me/Pro_XR)  
