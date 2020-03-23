using System;

namespace SoulsLike
{
    [Serializable]
    public class ActorStats
    {
        /// <summary>
        /// Actor ID
        /// </summary>
        public int actorID;
        
        /// <summary>
        /// Actor Name
        /// </summary>
        public string name;

        /// <summary>
        /// Actor Level
        /// </summary>
        public int level;

        /// <summary>
        /// Actor Position
        /// </summary>
        public Vec3 actorPosition;

        /// <summary>
        /// Actor Rotation
        /// </summary>
        public Quat actorRotation;

        /// <summary>
        /// Actor Species (i.e. Race)
        /// </summary>
        public ActorSpeciesType actorSpecies;

        /// <summary>
        /// Actor Faction
        /// </summary>
        public string actorFaction;

        /// <summary>
        /// Actor Fight Wait Time
        /// </summary>
        public float actorFightWait;

        /// <summary>
        /// Actor Dead?
        /// </summary>
        public bool isDead;

        /// <summary>
        /// Actor Health
        /// </summary>
        public float currentHealth;

        /// <summary>
        /// Actor Maximum Health
        /// </summary>
        public float maxHealth;

        /// <summary>
        /// Actor Current Magic
        /// </summary>
        public float currentMagic;

        /// <summary>
        /// Actor Maximum Magic
        /// </summary>
        public float maxMagic;

        /// <summary>
        /// Actor Current Stamina
        /// </summary>
        public float currentStamina;

        /// <summary>
        /// Actor Maximum Stamina
        /// </summary>
        public float maxStamina;

        /// <summary>
        /// Actor Current Toxins
        /// </summary>
        public float currentToxins;

        /// <summary>
        /// Actor Maximum Toxins
        /// </summary>
        public float maxToxins;

        /// <summary>
        /// Actor Sneaking?
        /// </summary>
        public bool isSneaking;

        /// <summary>
        /// Actor Fatigue?
        /// </summary>
        public float fatigue;

        /// <summary>
        /// Actor's short blade skill, dictates attack strength of short blade (i.e. short sword, dagger, etc)
        /// </summary>
        public int shortBlade;

        /// <summary>
        /// Actor's long blade skill, dictates attack strength of long blade (i.e. sword, katana, etc)
        /// </summary>
        public int longBlade;

        /// <summary>
        /// Actor's spear skill, dictates attack strength of spear
        /// </summary>
        public int spear;

        /// <summary>
        /// Actor's spear skill, dictates attack strength of blunt weapons (Club & mace)
        /// </summary>
        public int bluntWeapon;

        /// <summary>
        /// Actor's spear skill, dictates attack strength of axe's.
        /// </summary>
        public int axe;

        /// <summary>
        /// Actor's spear skill, dictates the attack of all weapons by a small amount and also determines max carry weight.
        /// </summary>
        public int strength;

        /// <summary>
        /// Actor's strength, dictates damage taken (higher = less damage)
        /// </summary>
        public int endurance;

        /// <summary>
        /// Actor's intelligence, dictates magic duration
        /// </summary>
        public int intelligence;

        /// <summary>
        /// Actor's sneak, dicates how well they can sneak w/o being detected.
        /// </summary>
        public int sneak;

        /// <summary>
        /// Actor's atletic skill, dictates how long they can sprint and how high they can jump
        /// </summary>
        public int athletic;

        /// <summary>
        /// Actor's light armour skill, dictates how well light armour reflects damage.
        /// </summary>
        public int lightArmour;

        /// <summary>
        /// Actor's medium armour skill, dictates how well medium armour reflects damage.
        /// </summary>
        public int mediumArmour;

        /// <summary>
        /// Actor's heavy armour skill, dictates how well heavy armour reflects damage.
        /// </summary>
        public int heavyArmour;

        /// <summary>
        /// Actor's charisma skill, dictates how well pursuastion is in dialogue and also affects prices (higher charisma = slightly lower prices)
        /// </summary>
        public int charisma;

        /// <summary>
        /// Actor's lockpicking skill, dictates the size of the "sweet spot" in the lock picking minigame.
        /// </summary>
        public int lockPicking;

        /// <summary>
        /// Actor's block skill, dictates how well armour blocks attacks (higher = less damage)
        /// </summary>
        public int block;

        /// <summary>
        /// Actor's alchemy skill, dictates how the strength of player made alchemy potions.
        /// </summary>
        public int alchemy;

        /// <summary>
        /// Actor's enchant skill, dictates how the strength of player made enchantments potions.
        /// </summary>
        public int enchant;

        /// <summary>
        /// Actor's luck, affects everything in a small way.
        /// </summary>
        public int luck;
    }
}
