using System;
using System.Collections.Generic;

namespace CRUD_Workout.Repositories
{
    public interface IWorkoutRepository
    {
        // Utwórz
        bool Create(Workout entity);

        // Przeczytaj wszytkie rekordy
        List<Workout> ReadAll();

        // Przeczytaj jeden rekord
        Workout Read(int Id);

        // Zaktualizuj
        bool Update(Workout entity);

        // Usuń
        bool Delete(Workout entity);
    }
}
