#include <stdio.h>
#include <stdlib.h>
#include "moon.c"

int main(int argc, char **argv)
{
    int moon_count;
    struct Moon **moons = read_system(argv[1], &moon_count);
    const int repetitions = atoi(argv[2]);

    int i = 0;
    fprintf(stdout, "After %d steps:\n", i);
    char *this_step = to_string_system(moon_count, moons);
    fprintf(stdout, "%s\n\n", this_step);
    free(this_step);
    while (i < repetitions) {
        ++i;
        gravity(moon_count, moons);
        velocity(moon_count, moons);
        fprintf(stdout, "After %d steps:\n", i);
        char *this_step = to_string_system(moon_count, moons);
        fprintf(stdout, "%s\n", this_step);
        free(this_step);
        if (i % 10 == 0) {
            fprintf(stdout, "Energy after %d steps:\n", i);
            int total_energies[moon_count];
            int total_energy = 0;
            for (int j = 0; j < moon_count; ++j) {
                int pot = abs(moons[j]->coord[0])    + abs(moons[j]->coord[1])    + abs(moons[j]->coord[2]);
                int kin = abs(moons[j]->velocity[0]) + abs(moons[j]->velocity[1]) + abs(moons[j]->velocity[2]);
                total_energies[j] = pot * kin;
                total_energy += total_energies[j];
                fprintf(stdout, "pot: %3d + %3d + %3d = %3d;    kin: %3d + %3d + %3d = %3d;    toal: %3d * %3d = %3d\n",
                    abs(moons[j]->coord[0]),    abs(moons[j]->coord[1]),    abs(moons[j]->coord[2]),    pot,
                    abs(moons[j]->velocity[0]), abs(moons[j]->velocity[1]), abs(moons[j]->velocity[2]), kin,
                    pot, kin, pot * kin
                );
            }
            fprintf(stdout, "Sum of total energy: %3d + %3d + %3d + %3d = %d\n",
                total_energies[0], total_energies[1], total_energies[2], total_energies[3], total_energy
            );
        }
        if (i < repetitions) {
            fprintf(stdout, "\n");
        }
    }

    for (int i = 0; i < moon_count; ++i) {
        free(moons[i]);
    }
    free(moons);

    return 0;
}