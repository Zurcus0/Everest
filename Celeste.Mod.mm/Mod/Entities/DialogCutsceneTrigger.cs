﻿using Microsoft.Xna.Framework;

namespace Celeste.Mod.Entities {
    [CustomEntity("everest/dialogTrigger", "dialog/dialogtrigger", "cavern/dialogtrigger")]
    public class DialogCutsceneTrigger : Trigger {

        private string dialogEntry;
        private bool triggered;
        private EntityID id;
        private bool onlyOnce;
        private bool endLevel;
        private int deathCount;

        public DialogCutsceneTrigger(EntityData data, Vector2 offset, EntityID entId)
            : base(data, offset) {
            dialogEntry = data.Attr("dialogId");
            onlyOnce = data.Bool("onlyOnce", true);
            endLevel = data.Bool("endLevel", false);
            deathCount = data.Int("deathCount", -1);
            triggered = false;
            id = entId;
        }

        public override void OnEnter(Player player) {
            if (triggered || (Scene as Level).Session.GetFlag("DoNotLoad" + id) ||
                (deathCount >= 0 && SceneAs<Level>().Session.DeathsInCurrentLevel != deathCount)) {

                return;
            }

            triggered = true;

            Scene.Add(new DialogCutscene(dialogEntry, player, endLevel));

            if (onlyOnce)
                (Scene as Level).Session.SetFlag("DoNotLoad" + id, true); // Sets flag to not load
        }

    }
}
