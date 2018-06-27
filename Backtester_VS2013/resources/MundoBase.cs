using netAvida.backend.interfaces;
using System;

namespace netAvida.backend
{
    public class MundoBase : IWorld
    {
        public MundoBase()
        {
            initSettings();
            taskControl = new TaskControl(this);
            mutation = new MutationControl(this);
            instructionManager = initInstructions();
            Log.info("Instructions: " + instructions.size());
            settings.getInstructionCount = instructions.size();

        }
    }

    
}
