using System;

namespace SoulsLike
{
    [Serializable]
    public class ActorStats
    {
        // Actor's public name
        public string name;

        // Actor level
        public int level;

        // Actor species
        public ActorSpeciesType actorSpecies;

        // Actor factions
        public string actorFaction;

        public float actorFightWait;

        // Actor isDead
        public bool isDead;

        // Actor's current health
        public float currentHealth;

        // Actor's maximum health
        public float maxHealth;

        // Actor isSneaking
        public bool isSneaking;

        // Actor fatigue
        public float fatigue;

        // Actor's short blade skill, dictates attack strength of short blade (i.e. short sword, dagger, etc)
        public int shortBlade;

        // Actor's long blade skill, dictates attack strength of long blade (i.e. sword, katana, etc)
        public int longBlade;

        // Actor's spear skill, dictates attack strength of spear
        public int spear;

        // Actor's spear skill, dictates attack strength of blunt weapons (Club & mace)
        public int bluntWeapon;

        // Actor's spear skill, dictates attack strength of axe's.
        public int axe;

        // Actor's spear skill, dictates the attack of all weapons by a small amount and also determines max carry weight.
        public int strength;

        // Actor's strength, dictates damage taken (higher = less damage)
        public int endurance;

        // Actor's intelligence, dictates magic duration
        public int intelligence;

        // Actor's sneak, dicates how well they can sneak w/o being detected.
        public int sneak;

        // Actor's atletic skill, dictates how long they can sprint and how high they can jump
        public int athletic;

        // Actor's light armour skill, dictates how well light armour reflects damage.
        public int lightArmour;

        // Actor's medium armour skill, dictates how well medium armour reflects damage.
        public int mediumArmour;

        // Actor's heavy armour skill, dictates how well heavy armour reflects damage.
        public int heavyArmour;

        // Actor's charisma skill, dictates how well pursuastion is in dialogue and also affects prices (higher charisma = slightly lower prices)
        public int charisma;

        // Actor's lockpicking skill, dictates the size of the "sweet spot" in the lock picking minigame.
        public int lockPicking;

        // Actor's block skill, dictates how well armour blocks attacks (higher = less damage)
        public int block;

        // Actor's alchemy skill, dictates how the strength of player made alchemy potions.
        public int alchemy;

        // Actor's enchant skill, dictates how the strength of player made enchantments potions.
        public int enchant;

        // Actor's luck, affects everything in a small way.
        public int luck;
    }
}
