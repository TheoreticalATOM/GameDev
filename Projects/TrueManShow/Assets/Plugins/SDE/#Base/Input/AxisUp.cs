
namespace SDE.Input
{
    public class AxisUp
    {
        public delegate bool DelAxisIsTrue(float input);

        private bool mIsInUse = false;
        private DelAxisIsTrue mEvaluationAction;

        public AxisUp()
        {
            mEvaluationAction = (input) => { return input != 0.0f; };
        }

        public AxisUp(DelAxisIsTrue evaluation)
        {
            mEvaluationAction = evaluation;
        }

        public float GetAxisUp(float input)
        {
            float valueToReturn = 0.0f;

            if (mEvaluationAction(input))
            {
                if (!mIsInUse)
                {
                    mIsInUse = true;
                    valueToReturn = input;
                }
            }
            else
                mIsInUse = false;

            return valueToReturn;
        }
    }
}