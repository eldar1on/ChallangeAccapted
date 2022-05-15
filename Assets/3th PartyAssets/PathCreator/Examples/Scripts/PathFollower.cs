using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;

        public Platform _platform;



        void Start() {

            speed = 0;

            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }


///  BU SCRIPTİ EDİTLEDİM!
///  Sebastian League adli youtube kanalından apardığım iş bu script 
///  temel olarak bezier path creation tutorialindendir aybalam.
///  Bu scripte benim eklediğim kısımlar, eventler ve hızı modifiye eden fonksiyonlardan ibaret.
///  Event kullanmam istendiği ve eventler burada olduğu içün script klasörüne ekledim.

        void OnEnable()
        {
            EventManager.OnClicked += Haste;
            Platform.Success += Win;
            Platform.Fail += ZeroSpeed;
            //PathFollower.StartTheGameAlready += StartTheGame;
            Platform.StartTheGameAlready += StartTheGame;
        }

        void OnDisable()
        {
            EventManager.OnClicked -= Haste;
            Platform.Success -= Win;
            Platform.Fail -= ZeroSpeed;
            Platform.StartTheGameAlready -= StartTheGame;
            
        }

        public void StartTheGame()
        {
            speed = 10;
        }

        void Win()
        {
            speed = 0f;
        }

        void ZeroSpeed()
        {
            speed = 0;
        }

        void Haste()
        {
            StartCoroutine("Hasted");
        }

        IEnumerator Hasted()
        {
            speed = speed * 2f;
            print("Hasted!");

            yield return new WaitForSeconds(5f);

            speed = speed * .5f;
        }


        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
            
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}