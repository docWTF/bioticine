using System.Collections.Generic;
using BehaviorTree;

public class BossBT : Tree
{
    public static float speed = 2f;
    public static float fovRange = 100f;
    public static float attackRange = 5f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform),
                new TaskAttack(transform,this),
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform),
            }),
        });

        return root;
    }
}
