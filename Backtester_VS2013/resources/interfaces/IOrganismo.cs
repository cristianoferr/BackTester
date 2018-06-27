using System;
using System.Collections.Generic;
using System.Text;

namespace netAvida.backend.interfaces
{
    public interface IOrganismo
    {

        void setCurrStack(int to);

        int getReg(int from);

        int getMemory(int reg);

        bool hasStarted{ get; set; }


    void addFitness(float f);

        bool setMemory(int regVal, int inst);

        void fillTemplate(int sp);

        int sizeBuffer { get; set; }

        void error();

        String dump();

        int somaInstructions();

        int searchTemplateFwd();

        int searchTemplateBwd();

        void setReg(int reg, int i);

        int ip { get; set; }

        IOrganismo child();

        void clearChild();

        void addFitness();

        void addChild();

        void fatalError();

        void push(int i);


        int pop();

        void decReg(int to);

        void next(int step);

        void incReg(int to);

        void criticalError();

        int getMemorySize();

        int sp();

        void setChild(IOrganismo child);

        Instruction getInstruction();
        Instruction getInstruction(int memoryInstr);

        int id { get; set; }

        int getCurrStack();

        int[] getBuffer();

        void kill();

        bool validate(IOrganismo container);

        float getError();

        void checkTick();

        void run();

        IOrganismo parent();

        bool isAlive();

        void save();

        int childCount { get; set; }

        Instruction getInstructionAt(int i);

        bool setMemory(int index, int v, bool punish);

        void clearParent();

        int getGeneration();

        void setParent(IOrganismo parent);

        void reset(int memSize, int sp);

        bool validate();

        void setStartPoint(int i);

        int age { get; set; }

        int oid();

        String getCode();
        int getColorCode();


    }

}
