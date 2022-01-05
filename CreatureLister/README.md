# Creature Lister

Will help you creating a list of creatures with their configured base values. This mod is thought to be used by modders
only, since it will not change anything about the game. It does only provide a new console command to run the mod and
let it create the complete listing of creature stats.

## Features

I was always looking for the best way to get the on prefab level configured damage values for all creatures in the game.
Especially this was interesting, when using custom made creatures, since for vanilla ones, you can also look up the
values in the [IG wiki](https://valheim.fandom.com/wiki/Creatures). But even for this wiki I don't like the way of
hiding some values that might be of interest, like the different levels of resistances.

For the fact of how creatures do damage, it turned out (which I didn't realize in the first place) that creatures don't
have damage values assigned to them, but they rather do damage by a lot of custom weapons that are just made for each
creature and from which they can choose to use one of them (taking cooldown into account) for trying to hit your Player
character. So, getting to the real damage values required to lookup the items assigned to the creatures and somehow try
to do an weighted average calculation.

### What it doesn't do

* it does NOT change anything about game play
* re-configure creatures values (you will need to use something else for that)
* it doesn't show damage values that are changed by mods
  like [CreatureLevelAndLootControl](https://valheim.thunderstore.io/package/Smoothbrain/CreatureLevelAndLootControl/)
  since this example mod does change the damage value on each hit of a creature

### How to use it

Note: to use console commands, you will need to start the game with ```-console``` option, see
also [Console_Commands at Valheim wiki](https://valheim.fandom.com/wiki/Console_Commands).

The mod provides a simple new console command: ```creature_lister_generate_defaults_file```

(Luckily you don't need to type the whole string, since with H&H updates IG pushed auto-complete for console commands).

When done, you will see a log message in the console that points you to the place it created the file (inside BepInEx
config folder).

### Example output

(this does show only 1 entry for 1 creature, the file will contain all creatures)

```
Greydwarf_Shaman:
  internalName: $enemy_greydwarfshaman
  faction: ForestMonsters
  group: ''
  health: 60
  damageModifiers:
    mBlunt: Normal
    mSlash: Normal
    mPierce: Normal
    mChop: Ignore
    mPickaxe: Ignore
    mFire: VeryWeak
    mFrost: Normal
    mLightning: Normal
    mPoison: Resistant
    mSpirit: Immune
  defeatSetGlobalKey: ''
  averageDamageTypes:
    mDamage: 0
    mBlunt: 0
    mSlash: 7
    mPierce: 0
    mChop: 0
    mPickaxe: 0
    mFire: 0
    mFrost: 0
    mLightning: 0
    mPoison: 15
    mSpirit: 0
  averageTotalDamage: 22
  averageTotalDamageToPlayer: 22
  allItemNamesContributingToDamage:
  - Greydwarf_shaman_attack
  - Greydwarf_attack
  defaultItemAndRandomWeaponsNames:
  - Greydwarf_shaman_attack
  - Greydwarf_attack
  - Greydwarf_shaman_heal
```

## Incompatible mods

You will still be able to run this mod with the incompatible mods, but the results for the creatures of these mods will
be invalid.

* RRRMonsters -> does contain some creatures that have not vanilla complaint creature attacks

## Changelog

* 0.1.1 -> fix for RRRMonsters, since it does contain some creatures that have not vanilla complaint creature attacks
* 0.1.0 -> initial release

## Contact

* https://github.com/FelixReuthlinger/CreatureLister
* Discord: Flux#0062 (you can find me around some of the Valheim modding discords, too)
