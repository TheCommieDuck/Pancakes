
namespace WaffleCat.Core.Input
{
    public class InputDevice<State>
    {
        //Holding all the possible inputs. That is, down, pressed (just down), up, and released (just up)

        //Allows to see key changes
        protected State previousState, currentState;

        //Base updating
        public void Update(State currentState)
        {
            this.previousState = this.currentState;

            this.currentState = currentState;
        }

    }
}
