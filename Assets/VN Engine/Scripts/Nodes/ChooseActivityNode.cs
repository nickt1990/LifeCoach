using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace VNEngine
{
    public class ChooseActivityNode : Node
    {
        // Always visible
        public float activityNumber = 1;
        public string activityName = "play";

        public ConversationManager conversation_to_start;

        // String stats, only visible if stat_type is set to String
        [HideInInspector]
        public List<float> seenScenes;
        [HideInInspector]
        public string sceneName = "";


        public override void Run_Node()
        {
            sceneName = String.Empty;
            activityNumber = StatsManager.Get_Numbered_Stat("intActivity");
            activityName = StatsManager.Get_String_Stat("strActivity");
            //EX: conversation_to_start = GameObject.Find("ArtActivityManager").GetComponent<ConversationManager>();

            switch (activityName)
            {
                case "study":
                    sceneName +=  "s_";
                    break;
                case "workout":
                    sceneName += "w_";
                    activityNumber += 600;
                    break;
                case "art":
                    sceneName += "a_";
                    activityNumber += 1200;
                    break;
                case "talk":
                    sceneName += "t_";
                    activityNumber += 1800;
                    break;
                case "play":
                    sceneName += "p_";
                    activityNumber += 2400;
                    break;
            }

            IsNewScene();
            seenScenes.Add(activityNumber);

            //sceneName += activityNumber;
            sceneName += 1;

            conversation_to_start = GameObject.Find(sceneName).GetComponent<ConversationManager>();

            StartCoroutine(Check_If_Actors_Have_Exited());

            Finish_Node();
        }

        private void IsNewScene()
        {
            while (seenScenes.Contains(activityNumber))
            {
                if (activityNumber != 200 && activityNumber != 400 && activityNumber != 600 &&
                        activityNumber != 800 && activityNumber != 1000 && activityNumber != 1200 &&
                        activityNumber != 1400 && activityNumber != 1600 && activityNumber != 1800 &&
                        activityNumber != 2000 && activityNumber != 2200 && activityNumber != 2400)
                {
                    activityNumber++;
                }
                else
                {
                    activityNumber = activityNumber - 199;
                }
            }

            return;
        }

        IEnumerator Check_If_Actors_Have_Exited()
        {
            while (ActorManager.exiting_actors.Count > 0)
            {
                yield return new WaitForSeconds(0.1f);
            }

            StartNewConversation();
            yield break;
        }

        public void StartNewConversation()
        {
            conversation_to_start.Start_Conversation();
            this.transform.GetComponentInParent<ConversationManager>().Finish_Conversation();
        }

        public override void Button_Pressed()
        {

        }

        public override void Finish_Node()
        {
            StopAllCoroutines();

            base.Finish_Node();
        }
    }
}