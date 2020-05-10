﻿public class SampleDefaultFlowController : PhaseFlowController
{
    protected override PhaseBaseNode CreateRootNode()
    {
        SampleHuntPhaseActionNode huntPhaseActionNode = new SampleHuntPhaseActionNode(2);

        return new PhaseSerialComposition
        (
            0,
            new SampleInitialPhaseActionNode(1), // Drone waits, main menu opens up, character enters to the scene and count down starts
            huntPhaseActionNode, // Gameplay starts
            new SampleHuntCatchPhaseCondNode // Mini game
            (
                3,
                new PhaseSerialComposition // 1 of 2 mini game result which is success condition
                (
                    4,
                    new SampleCatchSuccessPhaseActionNode(5), // Catch success, catch result pop up appears, game freezes, character dances etc
                    new SampleLevelEndPhaseCondNode // Checks whether game should finish or not
                    (
                        6,
                        new SampleLevelWinPhaseActionNode(7), // Game finish success, char makes another dance, dragon eats apple, level end success UI, success music plays
                        new SampleLevelFailPhaseActionNode(8), // Game finish fail, char makes sad dance, dragon kills us, level end fail UI, fail music plays
                        new PhaseGotoNode(9, huntPhaseActionNode) // game is not ready to finish (ie. not all bugs are catched, not all objecives are completed etc)
                    )
                ),
                new PhaseSerialComposition // other mini game result which is failure condition
                (
                    5,
                    new SampleCatchFailPhaseActionNode(10), // char fails to catch, or gets away from bug
                    new PhaseGotoNode(11, huntPhaseActionNode) // returns to hunt phase
                )
            )
        );
    }
}
