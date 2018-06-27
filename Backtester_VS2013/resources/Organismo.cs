using netAvida.backend.instructions;
using netAvida.backend.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace netAvida.backend
{
    public class Organismo : IOrganismo
    {
        private static int orgsCount = 0;
        private static int objCount = 0;


        private int oid = 0;
        public int id { get; set; }

        private int bonusEnergy = 0;
        protected IWorld mundo;
        private Pilha stacks;
        protected IOrganismo _child = null;
        protected int memoryPoolCount = 1; // Quantidade de pools, default é 1,
                                           // quando tiver filho é 2
        int currTask;
        int age;

        protected int[] buffer = new int[CONSTS.MAX_BUFFER];
        private float _error;

        private IOrganismo _parent;
        private int parentId = 0;
        private int colorCode = 0;

        private bool alive;
        private int childCount;
        private int lastChildCount;
        private int childlessCounter = 0;
        private int generation = 0;

        private bool wroteFlag;
        protected float tickError = 0;

        int somaInsts = 0;
        protected MutationControl mutation;
        protected int childId;
        bool _wroteFlag = false;


        protected void initMemory()
        {
        }


        public void reset(int memSize, int sp)
        {
            id = ++orgsCount;
            childId = 0;
            colorCode = 0;
            setMemorySize(memSize);
            setStartPoint(sp);
            initBuffer();
            initStacks();
            ip = sp;
            clearChild();
            _parent = null;
            hasStarted = false;
            _error = 0;
            age = 0;
            alive = true;
            bonusEnergy = 0;
            _wroteFlag = true;
            lastChildCount = 0;
            childlessCounter = 0;
            childCount = 0;
            somaInsts = 0;
            currTask = 0;

        }

        protected void setMemorySize(int memSize)
        {
            Log.fatal("Undefined setMemorySize");
        }

        public int getColorCode()
        {
            if (colorCode != 0)
            {
                return colorCode;
            }
            colorCode = somaInstructions() * 10;
            return colorCode;
        }

        private int difFromFather(int memorySize)
        {

            int code = somaInstructions() / memorySize;
            int parentSize = _parent.getMemorySize();
            if (parentSize < CONSTS.MIN_MEMORY_CHILD * memorySize)
            {
                parentSize = (int)(CONSTS.MIN_MEMORY_CHILD * memorySize);
            }
            int parentCode = _parent.somaInstructions() / parentSize;
            int dif = code - parentCode;
            return dif;
        }

        public void setStartPoint(int i)
        {
            Log.fatal("Undefined setMemorySize");
        }

        private void initStacks()
        {

            stacks = new Pilha(this);
        }

        protected void initRegs()
        {
            Log.fatal("Undefined initRegs");
        }

        private void initBuffer()
        {
            for (int i = 0; i < CONSTS.MAX_BUFFER; i++)
            {
                buffer[i] = 0;
            }
            sizeBuffer = 0;
        }


        public int sp()
        {
            Log.fatal("Undefined sp()");
            return 0;
        }


        public int ip
        {
            get
            {
                return getReg(CONSTS.IP_REG);
            }
            set
            {
                setReg(CONSTS.IP_REG, value);
            }
        }


        public bool setMemory(int index, int v)
        {
            return setMemory(index, v, true);
        }


        public void setReg(int i, int v)
        {
            Log.fatal("Undefined setReg");
        }


        public int getReg(int i)
        {
            Log.fatal("Undefined getReg");
            return 0;
        }


        public void push(int j)
        {
            stacks.push(j);

        }


        public int pop()
        {
            return stacks.pop();
        }

        public void decReg(int to)
        {
            setReg(to, getReg(to) - 1);
        }


        public void incReg(int to)
        {
            setReg(to, getReg(to) + 1);
        }


        public int getMemorySize()
        {
            Log.fatal("Undefined getMemorySize");
            return 0;
        }


        public bool setMemory(int index, int v, bool punish)
        {
            return false;
        }


        public Instruction getInstruction(int memoryInstr)
        {
            Instruction i = mundo.getInstruction(memoryInstr);
            return i;
        }


        public Instruction getInstruction()
        {
            int currentMemory = getCurrentMemory();
            return getInstruction(currentMemory);
        }

        protected int getCurrentMemory()
        {
            return getMemory(ip);
        }


        public void fillTemplate(int sp)
        {
            int mem = 0;
            int pos = ip + sp;
            sizeBuffer = 0;
            do
            {
                mem = getMemory(pos);
                if (mem == CONSTS.NOP0 || mem == CONSTS.NOP1)
                {
                    buffer[sizeBuffer] = (mem == CONSTS.NOP0 ? CONSTS.NOP1
                            : CONSTS.NOP0);
                    sizeBuffer++;
                }
                pos++;
            } while ((mem == CONSTS.NOP0 || mem == CONSTS.NOP1)
                    && sizeBuffer < CONSTS.MAX_BUFFER);
        }


        public int searchTemplateFwd()
        {
            if (sizeBuffer == 0)
            {
                return -1;
            }
            int indexBuffer = 0;
            for (int i = 0; i < CONSTS.TEMPLATE_LIMIT; i++)
            {
                int index = ip + i;
                int m = getMemory(index);
                if (m == buffer[indexBuffer])
                {
                    indexBuffer++;
                }
                else
                {
                    indexBuffer = 0;
                }
                if (indexBuffer == sizeBuffer)
                {// && getMemory(index + 1) > 1) {
                    return index + 1;
                }
            }
            return -1;
        }

        public string debugInfo(string prefix)
        {
            string saida = prefix + " = ";

            for (int i = 0; i < CONSTS.REGISTRADORES; i++)
            {
                saida += CONSTS.getLetter(i) + "X:"
                        + CONSTS.numberFormat(getReg(i)) + " ";
            }
            saida += "SP:" + CONSTS.numberFormat(sp()) + " IP:"
                    + CONSTS.numberFormat(ip) + " E:" + ((int)_error)
                    + " ID:" + id;
            if (_child != null)
            {
                saida += " (Child:" + _child.id + " size:"
                        + CONSTS.numberFormat(_child.getMemorySize()) + " )";
            }

            saida += "\n#" + stacks.debugInfo();
            return saida;
        }


        public string Tostring()
        {
            string ret = headerOutput();
            ret += getCode();
            return ret;
        }


        public string getCode()
        {
            return listInstructions(getMemorySize(), true);
        }

        protected string headerOutput()
        {
            string ret = "#jAvida	Id:" + id + " Gen:" + generation + " ChildCount:"
                    + childCount + " Age:" + age + "	Size: "
                    + getMemorySize() + "\n";
            ret += "#REGS: " + debugInfo("") + "\n";
            return ret;
        }

        protected string listInstructions(int limit, bool includeNoop)
        {
            string ret = "";
            int step = 1;
            int sp = sp();
            for (int i = sp; i < sp + limit; i += step)
            {
                step = 1;
                int currentMemory = getMemory(i);
                Instruction inst = getInstructionAt(i);

                if (inst != null)
                {
                    if (!(inst is NopInstruction) || includeNoop
                            || i < getMemorySize() * 1.2f) {
                string s = listInstruction(i, inst, currentMemory);
                ret += s;
            }
                step = inst.getStep();
            } else {
				string s = listInstruction(i, inst, currentMemory);
             ret += s;
			}
        }
		return ret;
	}

	protected string listInstruction(int i, Instruction inst, int currentMemory)
{
    string name = "";

    if (inst != null)
    {
        name = inst.getDescription(this, i);
    }
    else
    {
        name = "" + currentMemory;
    }
    string s = (i) + ":: [" + currentMemory + "] =" + name + "\n";
    if (ip == i)
    {
        s = ">" + s;
    }
    return s;
}

        

public int somaInstructions()
{
    if (somaInsts > 0)
    {
        return somaInsts;
    }
    int memorySize = getMemorySize();
    int sp = this.sp();
    for (int i = 0; i < memorySize; i++)
    {
        int mem = getMemory(sp + i);
        somaInsts += mem;
    }
    return somaInsts;
}


    public void save()
{
    ALifeIO.saveToFile(this);
}


    public int searchTemplateBwd()
{
    if (sizeBuffer == 0)
    {
        return -1;
    }
    int indexBuffer = sizeBuffer;
    for (int i = 0; i < CONSTS.TEMPLATE_LIMIT; i++)
    {
        int index = ip - i;
        int m = getMemory(index);
        if (m == buffer[indexBuffer - 1])
        {
            indexBuffer--;
        }
        else
        {
            indexBuffer = sizeBuffer;
        }
        if (indexBuffer == 0)
        {
            // if (getMemory(index - 1) > 1) {
            return index + sizeBuffer;
            // } else {
            // indexBuffer = sizeBuffer;
            // }
        }
    }
    return -1;
}


    public float getError()
{
    return tickError + _error;
}


    public void criticalError()
{
    criticalError(1);

}

protected void criticalError(int mult)
{
    _error += (mundo.settings().errorCritical * mult);
}


    public void fatalError()
{
    if (!alive)
    {
        return;
    }
    _error += mundo.settings().errorCritical;
    if (parent() != null)
    {
        parent().error();
    }
    mundo.dealloc(this);
}


    public void error()
{
    _error += mundo.settings().errorNormal;

}


    public void addFitness(float f)
{
    addEnergy(CONSTS.FITNESS_TO_ENERGY_RATIO * f);
    _error -= mundo.settings().fitnessNormal * f;

}


    public void addFitness()
{
    addFitness(1);
}


    public bool validate(IOrganismo memoryContainer)
{
    if (memoryContainer == null)
    {
        memoryContainer = this;
    }
    int memorySize2 = getMemorySize();
    memorySize2 = CONSTS.validateMemorySize(parent(), memorySize2);
    if (memorySize2 == 0)
    {
        parent().criticalError();
        return false;
    }
    bool hasMal = false;
    bool hasDivide = false;

    bool hasRequireTemplate = false;

    int sp = sp();
    int i = 0;
    int step = 1;
    while (i < memorySize2)
    {
        Instruction inst = memoryContainer.getInstructionAt(sp + i);
        if (inst != null)
        {
            step = inst.getStep();
            if ((inst instanceof MalInstruction)

                        || (inst instanceof ConnectInstruction)) {
                hasMal = true;
            }
            if ((inst instanceof DivideInstruction)

                        || (inst instanceof DisconnectInstruction)) {
                hasDivide = true;
            }
            if (inst.requiresTemplate())
            {
                hasRequireTemplate = true;
            }
        }
        else
        {
            step = 1;
        }
        i += step;
    }

    if (!hasMal || !hasDivide)
    {
        return false;
    }
    else
    {
        addFitness();
        return true;
    }

}


    public void addChild()
{
    childCount++;
    if (childCount == CONSTS.AUTO_SAVE_PROGRAM_WITH_CHILD_COUNT)
    {
        if (child != null)
        {
            child.save();
        }
    }

}


    public void kill()
{
    alive = false;
    clearChild();
    clearParent();

}


    public bool isAlive()
{
    return alive;
}


    public void run()
{
    tick();
    int min = 4 < bonusEnergy ? 4 : bonusEnergy;
    for (int i = 0; i < min; i++)
    {
        if (isAlive())
        {

            tick();
            bonusEnergy--;
        }
    }
    /*
     * for (int i = 0; i < 1 + bonusEnergy; i++) { if (!isAlive()) { return;
     * } tick(); } bonusEnergy = 0;
     */
    if (isAlive())
    {
        mutation.errorLimitAction(this);
        mutation.randomMutation(this);
    }
}

private void tick()
{
    if (id() == mundo.getViewer().selectedOrgId())
    {
        Log.trace("tick");
    }
    Instruction instruction = getInstruction();
    int step = 1;
    if (instruction != null)
    {
        instruction.executa(this);
        step = instruction.getStep();
    }
    else
    {
        criticalError();
    }
    next(step);
}


    public void checkTick()
{
    /*
     * if (id()==744658){ Log.info(lastChildCount+"="+childCount); }
     */

    if (lastChildCount == childCount)
    {
        childlessCounter++;
        if (childlessCounter > 3)
        {
            error();
        }
    }
    else
    {
        childlessCounter = 0;
    }

    if (!wroteFlag)
    {
        error();
        return;
    }

    if (error > CONSTS.ERROR_UPPER_LIMIT)
    {
        error = CONSTS.ERROR_UPPER_LIMIT;
    }
    if (error < -CONSTS.ERROR_UPPER_LIMIT)
    {
        error = -CONSTS.ERROR_UPPER_LIMIT;
    }

    wroteFlag = false;
    float childReward = (childCount - lastChildCount)
            * mundo.settings().childReward;
    if (childReward > 0)
    {
        addFitness(childReward);
    }

    // addFitness(getMemorySize() * mundo.settings().getSizeReward);
    lastChildCount = childCount;
    // if (getError()>AvidaConsts.ERROR_LIMIT){
    // addErrorCritical();

    // }

}

protected void punishSimilarity()
{

}


    public void setCurrStack(int to)
{
    currStack = CONSTS.calcIndex(to, CONSTS.STACKS);

}


    public int getMemory(int reg)
{
    Log.fatal("Undefined getMemory");
    return 0;
}


    public void wroteFlag()
{
    wroteFlag = true;

}

    public int sizeBuffer { get; set; }


    public IOrganismo child()
{
    if (child != null && child.id() != childId)
    {
        clearChild();
    }

    return child;
}


    public void clearChild()
{
    child = null;
    memoryPoolCount = 1;
}


    public void clearParent()
{
    if (parent != null && parent.child() == this)
    {
        parent.clearChild();
    }
    parent = null;
}


    public void next(int step)
{
    age++;
    setIp(ip + step);
}


    public void setChild(IOrganismo child)
{

    this.child = child;
    this.childId = child.id();
    memoryPoolCount = (child == null ? 1 : 2);
    if (this.child == this)
    {
        Log.fatal("Erro!!!");
    }
}


    public int getCurrStack()
{
    return currStack;
}


    public int[] getBuffer()
{
    return buffer;
}


    public IOrganismo parent()
{
    if (parent != null && parentId != parent.id)
    {
        parent = null;
    }
    return parent;
}

        

    public Instruction getInstructionAt(int pos)
{
    int currentMemory = getMemory(pos);
    return getInstruction(currentMemory);
}


    public int getGeneration()
{
    return generation;
}


    public void setParent(IOrganismo parent)
{
    parentId = parent.id();
    this.generation = parent.getGeneration() + 1;
    this.parent = parent;
}


    public void setPos(int x, int y)
{
}


    public int getX()
{
    return 0;
}

public string dump()
{
    return tostring();
}


    public bool validate()
{
    return validate(this);
}


    public int oid()
{
    return oid;
}

    public bool hasStarted { get; set; }


    public int incTask()
{
    currTask++;
    if (currTask > mundo.io().countTask())
    {
        currTask = 0;
    }
    return currTask;
}


    public int getTask()
{
    return currTask;
}


    public void addEnergy(float e)
{
    bonusEnergy += e;
}


    public void markTaskComplete(int taskId)
{
    completedTasks.add(taskId);

}


    public bool hasCompletedTask()
{
    return completedTasks.contains(currTask);
}


    public IOrganismo getNeighbourAt(int index)
{
    Log.fatal("getNeighbourAt not implemented");
    return null;
}


    public int getEnergy()
{
    return bonusEnergy;
}


    public void trasnferToIndex(int index)
{
    Log.fatal("trasnferToIndex not implemented");
}
}
