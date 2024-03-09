using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

/*
   File: LostGame.cs
   Last Modified: March 9, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Handles the EndGame Scene.
/// </summary>
public class EndGame : MonoBehaviour
{
    /// <summary>
    /// The time to wait till moving to the Credits Scene.
    /// </summary>
    [ SerializeField ] private float waitTime = 10;

    /// <summary>
    /// The TextMeshProUGUI component that will hold the player's final score.
    /// </summary>
    private TextMeshProUGUI finalScoreText;

    /// <summary>
    /// The TextMeshProUGUI component that will hold the player's final score.
    /// </summary>
    private TextMeshProUGUI WonLostText;

    /// <summary>
    /// Represents a pseudo-random number generator.
    /// </summary>
    private System.Random rnd = new System.Random();

    /// <summary>
    /// A list of responses to losing game at stage 0.
    /// </summary>
    /// <typeparam name="string">A response.</typeparam>
    private List< string > lost0Texts = new List<string>( )
    { 
        "Hold Sequence: Discrepancies found in pre-launch protocols; time to regroup and adjust.",
        "Alert: Pre-Launch Anomaly Detected. Ensuring astronaut safety; enhance your aerospace knowledge.",
        "Pre-Launch Status: Incomplete. Key components need scrutiny; your skills are crucial for success.",
        "Mission Pause: Review shows unsatisfactory conditions; refine your strategies for the next launch.",
        "Caution: Launch Prep Questioned. Reassess and deepen knowledge for future mission success.",
        "Alert: Check Failure. Systems didn't meet standards; prepare diligently for the next attempt.",
        "Mission Hold: Criteria Unmet. Your insights and adjustments are vital for the next journey.",
        "Warning: Launch Aborted. Focus and expertise needed for a successful retry.",
        "Standby: Verification Failed. Regroup and enhance your mission understanding.",
        "Caution: Pre-Launch Not Cleared. Further tuning and knowledge are essential for liftoff."
    };

    /// <summary>
    /// A list of responses to losing game at stage 1.
    /// </summary>
    /// <typeparam name="string">A response.</typeparam>
    private List< string > lost1Texts = new List<string>( )
    { 
        "Mission Abort: LAS Engaged, Astronauts Safe. Your insights are vital for our next attempt.",
        "Alert: LAS Activated, Crew Safe. Your commitment is key to our future success.",
        "Warning: Launch Aborted, Crew Secure. We rely on your resilience for the journey ahead.",
        "Hold: Crew Safe After LAS. We're reminded of safety's value; let's refocus for the next mission.",
        "Alert: Launch Interrupted, Crew Safe. A setback faced, but we'll aim for the stars again together.",
        "Critical Action: LAS Triggered, Crew Safe. Your ongoing support is crucial as we remain vigilant.",
        "System Alert: LAS Success, Astronauts Secure. Your role is central in our mission refinement.",
        "Emergency Abort: Crew Protected by LAS. Your insights are invaluable for future missions.",
        "Launch Halted: LAS Ensures Crew Safety. Your expertise is crucial as we address this challenge.",
        "Mission Update: Launch Aborted, Crew Recovering. With LAS integrity confirmed, we advance together."
    };

    /// <summary>
    /// A list of responses to losing game at stage 2.
    /// </summary>
    /// <typeparam name="string">A response.</typeparam>
    private List< string > lost2Texts = new List<string>( )
    { 
        "Mission Alert: Stage Separation Issue. Let's aim for a smoother transition next time.",
        "Alert: Orbit Exit Failed. Analyze and prepare for the next try.",
        "Warning: Separation and Exit Unachieved. Refine our approach for renewed vigor.",
        "Brief: Incomplete Orbit Exit. Your expertise is crucial for future success.",
        "Note: Failed Stage Separation. Enhance our strategies for the next launch.",
        "Stage Separation Halted, Let's recalibrate for future success.",
        "Orbit Exit Obstructed, Time for a strategic reassessment.",
        "Mission Stalled at Separation, Your insight is key to overcoming this.",
        "Orbit Departure Unsuccessful, A new attempt awaits your expertise.",
        "Stage Transition Failed, Ready for another challenge with improved planning."
    };

    /// <summary>
    /// A list of responses to losing game at stage 3.
    /// </summary>
    /// <typeparam name="string">A response.</typeparam>
    private List< string > lost3Texts = new List<string>( )
    { 
        "Lunar Transit Interrupted, Enhance navigational strategies for success.",
        "Orion Journey Halted, Deep space expertise is crucial for progress.",
        "Mission Alert: Lunar Approach Unsuccessful, Time to refine our trajectory calculations.",
        "Transit Failure: Life Support and Navigation Systems Need Review, Your technical acumen is essential.",
        "Orion's Moonward Trek Stalled, Reassess and fortify our lunar transit protocols.",
        "Orion's Lunar Transit Incomplete, Focus on technology enhancement for retry.",
        "Navigation Setback During Lunar Journey, Precision is key for the next attempt.",
        "Orbit to Moon Transit Disrupted, Delve deeper into our systems for solutions.",
        "Lunar Course Detour, Strengthen our space travel methodologies.",
        "Spacecraft Transit Halt, Optimize life support and navigation for future missions."
    };

    /// <summary>
    /// A list of responses to losing game at stage 4.
    /// </summary>
    /// <typeparam name="string">A response.</typeparam>
    private List< string > lost4Texts = new List<string>( )
    { 
        "Lunar Insertion Unachieved, Sharpen focus on orbital mechanics for improvement.",
        "Orion's Orbit Entry Faltered, Precision in lunar navigation is crucial.",
        "HLS Transfer Missed, Enhance understanding of lunar descent protocols.",
        "Orbit to Surface Transition Incomplete, Delve into HLS intricacies for mastery.",
        "Lunar Descent Halted, Refine strategies for tackling lunar gravitational challenges.",
        "Lunar Orbit Insertion Failed, Reassess and adapt our approach for precision.",
        "Descent Sequence Aborted, Intensify HLS prep for successful lunar touchdown.",
        "Orion's Lunar Engagement Interrupted, Critical analysis required for orbit accuracy.",
        "HLS Docking Unsuccessful, Strengthen mission protocols for seamless transfer.",
        "Failed Lunar Descent, Enhance landing site analysis for future missions."
    };

    /// <summary>
    /// A list of responses to losing game at stage 5.
    /// </summary>
    /// <typeparam name="string">A response.</typeparam>
    private List< string > lost5Texts = new List<string>( )
    { 
        "Lunar Touchdown Incomplete, Refocus on descent dynamics for success.",
        "Moon Landing Aborted, Critical review needed for landing protocols.",
        "Surface Exploration Halted, Enhance prep for lunar terrain analysis.",
        "Experiment Deployment Failed, Address technicalities for next attempt.",
        "Astronauts' Lunar Mission Interrupted, Prioritize safety and procedure reassessment.",
        "Artemis vs. Apollo Sites Unreached, Deepen historical and technical comparison understanding.",
        "Lunar Data Collection Incomplete, Strengthen experiment and exploration tactics.",
        "Moonwalk Mission Unfulfilled, Revisit astronaut training for surface operations.",
        "Landing Site Navigation Error, Refine lunar mapping for accurate descent.",
        "Lunar Base Setup Delayed, Optimize strategies for habitat and infrastructure establishment."
    };

    /// <summary>
    /// A list of responses to losing game at stage 6.
    /// </summary>
    /// <typeparam name="string">A response.</typeparam>
    private List< string > lost6Texts = new List<string>( )
    { 
        "Return Launch Failed, Reassess lunar lift-off protocols for clarity.",
        "Orion Rendezvous Missed, Enhance docking procedures and training.",
        "Lunar Escape Incomplete, Refine trajectory calculations for Earth return.",
        "Re-Entry Procedure Flawed, Focus on atmospheric entry angle and heat shield integrity.",
        "Earth Reacquisition Unsuccessful, Improve ground communication for re-entry guidance.",
        "Moon Departure Error, Strengthen understanding of lunar gravitational effects.",
        "Orbital Docking Unachieved, Enhance precision in spacecraft alignment and approach.",
        "Atmospheric Entry Aborted, Address re-entry protocols and spacecraft stability.",
        "Return Trajectory Miscalculated, Optimize navigation systems for accurate Earth approach.",
        "Recovery Operation Delayed, Ensure readiness of recovery teams and equipment."
    };

    /// <summary>
    /// A list of responses to losing game at stage 7.
    /// </summary>
    /// <typeparam name="string">A response.</typeparam>
    private List< string > lost7Texts = new List<string>( )
    { 
        "Re-entry Protocol Failure, Analyze and enhance thermal protection systems.",
        "Splashdown Missed, Refocus on precision in re-entry calculations.",
        "Recovery Operation Unsuccessful, Strengthen coordination with naval forces.",
        "Orion Capsule Retrieval Delayed, Optimize search and recovery strategies.",
        "Atmospheric Entry Error, Review and correct trajectory alignment procedures.",
        "Splashdown Target Inaccuracy, Improve landing zone predictions and tracking.",
        "Recovery Team Coordination Fault, Enhance communication and operational synergy.",
        "Capsule Water Landing Compromised, Address issues in descent and flotation systems.",
        "Post-Splashdown Procedure Flaw, Refine astronaut extraction and medical protocols.",
        "Historical Recovery Analysis Needed, Utilize past missions to improve current techniques."
    };

    /// <summary>
    /// A list of responses to winning game.
    /// </summary>
    /// <typeparam name="string">A response.</typeparam>
    private List< string > wonTexts = new List<string>( )
    { 
        "Artemis Triumph! Astronauts have safely splashed down, heralding a new era of lunar exploration.",
        "Congratulations! The Artemis crew's successful recovery underlines humanity's boundless potential in space.",
        "Artemis Victory! Our team's diligence ensures the crew's safe return, paving the way for future lunar missions.",
        "Homecoming Success! The Artemis astronauts are back on Earth, thanks to our collective expertise and passion.",
        "Artemis Achieved! The safe splashdown of our astronauts marks a monumental milestone in lunar exploration.",
        "Artemis Glorious Return! Our astronauts have touched down safely, advancing our lunar legacy.",
        "Celebrating Artemis! The crew's safe recovery embodies the spirit of exploration and sets the stage for further lunar discovery.",
        "Artemis Milestone Achieved! The successful splashdown caps a landmark mission, illuminating our path back to the Moon and beyond.",
        "Triumph of Artemis! The astronauts' safe return celebrates our commitment to exploring new lunar frontiers.",
        "Artemis Homecoming! With our astronauts back and mission accomplished, we stand on the brink of a new lunar era."
    };

    /// <summary>
    /// Sets the final player score to UI and waits a certain amount of time before switching to the Credits Scene.
    /// </summary>
    void Start( )
    {
        int i;
        finalScoreText = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "FinalScore", scriptName: "EndGame.cs" );
        finalScoreText.text = SaveSystem.GetInt( name: "PlayerScore" ).ToString( );
        WonLostText = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "WonLostText", scriptName: "EndGame.cs" );
        if( SaveSystem.GetBool( name: "Won" ) == true )
        {
            i = rnd.Next( minValue: 0, maxValue: wonTexts.Count - 1 );
            WonLostText.text = wonTexts[ index: i ];
        }
        else
        {
            switch ( GameManager.endGameStageNumber )
            {
                case 0:
                    i = rnd.Next( minValue: 0, maxValue: lost0Texts.Count - 1 );
                    WonLostText.text = lost0Texts[ index: i ];
                    break;
                case 1:
                    i = rnd.Next( minValue: 0, maxValue: lost1Texts.Count - 1 );
                    WonLostText.text = lost1Texts[ index: i ];
                    break;
                case 2:
                    i = rnd.Next( minValue: 0, maxValue: lost2Texts.Count - 1 );
                    WonLostText.text = lost2Texts[ index: i ];
                    break;
                case 3:
                    i = rnd.Next( minValue: 0, maxValue: lost3Texts.Count - 1 );
                    WonLostText.text = lost3Texts[ index: i ];
                    break;
                case 4:
                    i = rnd.Next( minValue: 0, maxValue: lost4Texts.Count - 1 );
                    WonLostText.text = lost4Texts[ index: i ];
                    break;
                case 5:
                    i = rnd.Next( minValue: 0, maxValue: lost5Texts.Count - 1 );
                    WonLostText.text = lost5Texts[ index: i ];
                    break;
                case 6:
                    i = rnd.Next( minValue: 0, maxValue: lost6Texts.Count - 1 );
                    WonLostText.text = lost6Texts[ index: i ];
                    break;
                case 7:
                    i = rnd.Next( minValue: 0, maxValue: lost7Texts.Count - 1 );
                    WonLostText.text = lost7Texts[ index: i ];
                    break;
                default:
                    WonLostText.text = "No stage number detected. See EndGame.cs to fix.";
                    break;
            }
        }
        WaitForCredits( );
    }

    /// <summary>
    /// Method that starts a coroutine for the IENumerator to wait a set time before moving to the Credits Scene.
    /// </summary>
    private void WaitForCredits( )
    {
        StartCoroutine( routine: WaitForCreditsNumerator( ) );
    }

    /// <summary>
    /// Waits for a set time before moving to the Credits Scene.
    /// </summary>
    private IEnumerator WaitForCreditsNumerator( )
    {
        yield return new WaitForSeconds( seconds: waitTime );
        SceneTransitions.CreditsScene( );
    }
}