# Audio Design Audit — Time-Lost-Knight

## Проектный контекст (выведено из кода)
- Жанр (по механикам FSM, платформингу, ближнему/дальнему бою): **2D action-platformer / metroidvania-like**.
- Сеттинг (по названиям ассетов и сцен): **тёмные катакомбы + фэнтези-экшен**.
- Главный герой: **рыцарь с ближними атаками, рывками, прыжками, wall-механиками и интеракциями**.
- Ключевые механики: перемещение, прыжок/стенка, дэш (forward + omni unlock), melee/ranged атака, интеракции, телепортеры, урон/смерть, пауза/попапы.

## Звуковая иерархия (чтобы избежать конфликтов)
1. **Critical Gameplay (приоритет 100):** урон, смерть, парирование/хит, телепорт, анлок способности.
2. **Player Foley (80):** шаги, прыжки, приземления, стенка, дэш.
3. **Combat Layers (70):** замах/свинг/удар/промах/попадание, выстрел/взрыв.
4. **Interactables (60):** сундук, аптечка, двери, pickup.
5. **UI Feedback (50):** hover/click/open/close, end game popup.
6. **Ambient Beds (30):** локационный шум катакомб, процедурные one-shot эмиттеры.

> Рекомендация по миксу: sidechain ambience от critical/combat-шины на 2–4 dB, чтобы экшен не тонул.

## Таблица аудиоинтеграции

| Категория | Триггер (Событие в коде) | Название звука (Asset Name) | Описание и характер звука | Технические нюансы (3D/2D, рандом) |
|---|---|---|---|---|
| Player / State | `PlayerMoveState.Update()` при `xInput != 0` + заземлении из `PlayerGroundState.DoCheck()` | `PLY_Footstep_Stone_Run_01` | Тяжёлые шаги латного героя по камню, короткий транзиент + низко-средний «топ» | 3D, random pitch ±0.08, random vol ±1.5 dB, 4–6 вариаций |
| Player / State | `PlayerIdleState.Enter()/Update()` (переход в idle) | `PLY_Armor_Idle_Rustle_Loop` | Тихий «броня/ткань» loop, редкая пульсация | 3D, очень тихо, loop, LPF при паузе |
| Player / State | `PlayerJumpState.Enter()` (`SetVelocityY`) | `PLY_Jump_Takeoff_01` | Короткий «вуф» + бряцание экипировки | 3D, one-shot, pitch ±0.05 |
| Player / State | `PlayerAirState.Update()` при вертикальном падении (`currentVelocity.y < 0`) | `PLY_Fall_AirRush_Light` | Лёгкий свист воздуха, нарастающий по скорости | 3D, loop с RTPC по скорости, high-pass автоматизацией |
| Player / State | `PlayerLandState.Update()` после air state | `PLY_Land_Heavy_01` | Плотное приземление: удар сапог + броня | 3D, one-shot, громкость от |vY| |
| Player / State | `PlayerWallSlideState.Update()` | `PLY_WallSlide_Stone_Scrape_Loop` | Скрежет брони/обуви по стене | 3D, loop, random start offset |
| Player / State | `PlayerWallGrabState.Enter()` | `PLY_WallGrab_ClothGrip_01` | Короткий хват рукой/перчаткой | 3D, one-shot |
| Player / State | `PlayerWallClimbState.Update()` | `PLY_WallClimb_Grunt_Soft` | Цикличные усилия + трение | 3D, loop segments, pitch ±0.06 |
| Player / State | `PlayerWallJumpState.Enter()` | `PLY_WallJump_Burst_01` | Энергичный отскок от стены | 3D, one-shot, transient emphasized |
| Player / State | `PlayerLedgeClibmState.TriggerAnimation()/FinishAnimation()` | `PLY_LedgeGrab_01`, `PLY_LedgeClimb_End_01` | Захват края + подтягивание корпуса | 3D, два маркера по animation events |
| Player / State | `PlayerDropDownState.Enter()` (`SetIgnorePlatform`) | `PLY_DropThrough_Platform_01` | Короткий соскок вниз через платформу | 3D, one-shot |
| Ability / Dash | `PlayerBaseDashState.Enter()` (start hold or instant) | `PLY_Dash_Start_01` | Резкий импульс воздуха/энергии | 3D, one-shot, slight doppler-off |
| Ability / Dash | `PlayerOmnidirectionalDashState` в hold-фазе (`timeScale`) | `PLY_Dash_Aim_Hum_Loop` | Напряжённый «заряд»/hum при выборе направления | 2D/3D hybrid, loop, duck ambience |
| Ability / Dash | `PlayerBaseDashState.Update()` при `StartDashMove()` | `PLY_Dash_Whoosh_01` | Широкий whoosh вдоль траектории | 3D, one-shot, pitch от длины вектора |
| Ability / Dash | `PlayerBaseDashState.Update()` при окончании `dashTime` | `PLY_Dash_End_Tail_01` | Короткий хвост/рассечение воздуха | 3D, one-shot |
| Combat / Melee | `PlayerAttackState.Enter()` + `Weapon.EnterWeapon()` | `WP_Sword_Windup_01` | Подготовка/замах мечом | 3D, one-shot, pitch ±0.04 |
| Combat / Melee | `WeaponAnimationToWeapon.AnimationActionTriger()` -> `AttackingWeapon.CheckMeleeAttack()` | `WP_Sword_Swing_01` | Ядро удара (свинг) средней длины | 3D, one-shot, 3+ вариации |
| Combat / Melee | `WeaponHitboxToWeapon.OnTriggerEnter2D()` | `WP_Hit_Flesh_01` / `WP_Hit_Stone_01` | Попадание по врагу/твёрдой поверхности | 3D, surface switch, random pitch ±0.07 |
| Combat / Melee | `WeaponHitboxToWeapon.OnTriggerExit2D()` / пустой детект | `WP_Swing_Miss_01` | Лёгкий просвист промаха | 3D, one-shot, тихий |
| Combat / Melee | `Weapon.AnimationFinishTrigger()` / `ExitWeapon()` | `WP_Attack_Recovery_Cloth` | Завершение связки, «сброс» стойки | 3D, one-shot |
| Combat / Ranged | `PlayerRangedAttackState.TriggerAnimation()` instantiate projectile | `WP_Ranged_Cast_01` | Короткий release/выброс энергии | 3D, one-shot |
| Combat / Projectile | `BaseProjectile.Initialize()` | `PRJ_Launch_Arrow_01` / `PRJ_Launch_Laser_01` | Старт полёта (в зависимости от типа снаряда) | 3D, one-shot, вариативный pitch |
| Combat / Projectile | `BaseProjectile.OnTriggerEnter2D()` | `PRJ_Impact_01` | Контакт с целью/поверхностью | 3D, one-shot |
| Combat / Projectile | `ExplosionProjectile.DestroyProjectile()` + `HitInRadius()` | `PRJ_Explosion_01` | Взрыв: sub + debris + tail | 3D, one-shot, расстояние-зависимый LPF |
| Enemy / State | `EnemyFirst...`/`EnemyTwo...`/`EnemyThree...`/`EnemyFour...` `Idle/Move/Attack` states | `ENM_Footstep_*`, `ENM_Attack_Windup_*`, `ENM_Vocal_*` | Индивидуальные шаги/рыки/атаки по архетипам | 3D, отдельные bus per enemy type |
| Enemy / Animation | `AnimationToFSM.TriggerAnimation()/FinishAnimation()` | `ENM_Attack_Release_01` / `ENM_Attack_End_01` | Слои в ключевых кадрах анимаций атак | 3D, строго по animation events |
| Health / Damage | `HealthComponent.TakeDamage()` и `valueChanged` | `CHR_Hurt_01` | Реакция на получение урона | 3D, one-shot, anti-spam cooldown 80–120 ms |
| Health / Death | `HealthComponent.died` (см. `EndGameWindow`, уничтожение) | `CHR_Death_01` | Смерть персонажа/юнита, более длинный хвост | 3D (мир) + 2D stinger (player death) |
| Physics | `FallDamage.Update()` при завершении падения и уроне | `PHY_FallDamage_Impact_01` | Тяжёлый удар после большого падения | 3D, громкость от накопленного `fallTime` |
| Physics / Breakable | `DamageableWall.Destroy()` + particles | `ENV_WallBreak_Stone_01` | Разрушение стены: треск + осыпание | 3D, one-shot, 2–3 вариации |
| Interactions | `PlayerInteractor.Update()` при `focused.OnFocusGained()` | `UI_WorldPrompt_Focus_01` | Подсветка интеракта, мягкий тик | 2D, one-shot |
| Interactions | `PlayerInteractor.Update()` при `focused.Interact()` | `INT_Use_Generic_01` | Подтверждение взаимодействия | 2D/3D hybrid |
| Interactions | `InteractionAudioHandler.Notify()` | `INT_Configured_OneShot` | Событийный звук из `InteractionAudioConfig` | 3D, громкость из SO |
| Interactions | `Chest.Open()` | `INT_Chest_Open_01`, `INT_Loot_Sparkle_01` | Открытие сундука + лут-акцент | 3D, двухслойный one-shot |
| Interactions | `FirstAidKit.Heal()` + `Destroy(gameObject)` | `INT_Pickup_Heal_01` | Подбор/исцеление (чистый, «светлый») | 3D, one-shot, лёгкий shimmer |
| Interactions | `DashAbilityUnlock.Unlock()` | `INT_AbilityUnlock_Dash_01` | Получение новой способности (важный джингл) | 2D stinger + 3D source |
| Interactions | `OmniDashAbilityUnlock.Unlock()` | `INT_AbilityUnlock_Omni_01` | Более редкий/мощный unlock джингл | 2D stinger, приоритет high |
| Teleport | `DoorTeleporterCore.OnTriggerEnter2D()` | `INT_Door_Enter_Whoosh_01` | Вход в дверь-телепорт | 3D, one-shot |
| Teleport | `TeleportMover.Move()` / `TeleportNotifier.Notify()` | `INT_Teleport_Travel_01`, `INT_Teleport_Arrive_01` | Исчезновение и материализация | 3D, двойной one-shot, short reverb |
| Trap / Checkpoint | `SpikesTeleportCheckpoint.SetNewTeleportPoint()` | `INT_Checkpoint_Set_01` | Подтверждение смены чекпоинта | 2D/3D hybrid |
| UI / Menu | `UIInputHandler.OnPause()` + `PauseWindow.OpenPause()` | `UI_Pause_Open_01` | Открытие паузы, «мягкий стоп-кадр» | 2D, one-shot, duck music -4 dB |
| UI / Menu | `PauseWindow.ClosePause()` | `UI_Pause_Close_01` | Возврат в игру | 2D, one-shot |
| UI / Popup | `Popup.Show()` (DOTween enter + buttons) | `UI_Popup_Open_01`, `UI_Button_Appear_01` | Появление окна и каскад кнопок | 2D, one-shot + серия коротких |
| UI / Popup | `Popup.Hide()` | `UI_Popup_Close_01` | Закрытие окна | 2D, one-shot |
| UI / Buttons | `UIMainMenuRootBinder.HangleGoToGameplayButtonClick()` | `UI_Click_Play_01` | Подтверждение старта | 2D, one-shot |
| UI / Buttons | `UIMainMenuRootBinder.HangleExitGameButtonClick()` | `UI_Click_Exit_01` | Подтверждение выхода | 2D, one-shot |
| UI / Buttons | hover (добавить в EventTrigger для кнопок) | `UI_Hover_01` | Короткий «тик» наведения | 2D, без рандома |
| UI / Endgame | `EndGameWindow.Show()` на `HealthComponent.died` | `UI_GameOver_Stinger_01` | Эмоциональный stinger game over | 2D, high priority |
| Ambience / Scene | `Scenes/Levels/Level 1`, `Level_1`, `Gameplay`, ассеты `catacombs` | `AMB_Catacombs_Base_Loop` | Низкий гул подземелья, влажные хвосты | 2D bed + 3D emitters |
| Ambience / Scene | зоны с дверями/телепортом | `AMB_MagicDoor_Hum_Loop` | Еле заметный магический гул у порталов | 3D loop, attenuation 6–12м |
| Ambience / Scene | разрушаемые стены/ловушки | `AMB_DustRattle_OneShots` | Редкие каменные осыпи, микродвижение среды | 3D one-shots, random interval |

## Дополнительные технические рекомендации
- Для **шагов/ударов/попаданий** использовать минимум 4 варианта + микрорэндом громкости/питча.
- Для событий через анимации (`AnimationTrigger`, `AnimationFinishTrigger`) ставить звук в **конкретные ключевые кадры**.
- Для всей UI группы использовать отдельный **2D UI Bus**, для мира — **SFX 3D Bus**.
- Для телепорта и анлоков добавить короткий send в reverb (0.2–0.6s), чтобы подчеркнуть «магичность».
- Для player death: короткий freeze + UI stinger + приглушение ambience/music (snapshot transition 120–200 ms).
