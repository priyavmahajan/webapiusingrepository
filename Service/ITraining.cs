using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ITraining
    {
        IEnumerable GetTraining() ;
        IEnumerable GetTrainingByID(int id);
        void Insert(Models.TrainingModel training);
        void Delete(int id);
        void Update(int id, Models.TrainingModel training);

    }
}
