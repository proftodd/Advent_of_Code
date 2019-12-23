#include <assert.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

struct Moon {
    int x, y, z;
    int vel_x, vel_y, vel_z;
};

void gravity(int moon_count, struct Moon moons[])
{
    for (int i = 0; i < moon_count; ++i) {
        for (int j = i + 1; j < moon_count; ++j) {
            if (moons[i].x < moons[j].x) {
                ++moons[i].vel_x;
                --moons[j].vel_x;
            } else if (moons[i].x > moons[j].x) {
                --moons[i].vel_x;
                ++moons[j].vel_x;
            }
            if (moons[i].y < moons[j].y) {
                ++moons[i].vel_y;
                --moons[j].vel_y;
            } else if (moons[i].y > moons[j].y) {
                --moons[i].vel_y;
                ++moons[j].vel_y;
            }
            if (moons[i].z < moons[j].z) {
                ++moons[i].vel_z;
                --moons[j].vel_z;
            } else if (moons[i].z > moons[j].z) {
                --moons[i].vel_z;
                ++moons[j].vel_z;
            }
        }
    }
}

void velocity(int moon_count, struct Moon moons[])
{
    for (int i = 0; i < moon_count; ++i) {
        moons[i].x += moons[i].vel_x;
        moons[i].y += moons[i].vel_y;
        moons[i].z += moons[i].vel_z;
    }
}

int main(int argc, char **argv)
{
    const char *format = "<x=%d, y=%d, z=%d>\n";
    const int MOON_COUNT = 4;
    struct Moon moons[MOON_COUNT];

    const char *filename = argv[1];
    const int repetitions = atoi(argv[2]);

    FILE *file = fopen(argv[1], "r");
    assert(file != NULL);
    char current_line[100];
    int moon_count = 0;
    while (fgets(current_line, sizeof(current_line), file) != NULL) {
        int x, y, z;
        sscanf(current_line, format, &x, &y, &z);
        moons[moon_count].x = x;
        moons[moon_count].y = y;
        moons[moon_count].z = z;
        moons[moon_count].vel_x = 0;
        moons[moon_count].vel_y = 0;
        moons[moon_count].vel_z = 0;
        ++moon_count;
    }
    fclose(file);

    int i = 0;
    fprintf(stdout, "After %d steps:\n", i);
    for (int j = 0; j < MOON_COUNT; ++j) {
        fprintf(stdout, "pos=<x=%3d, y=%3d, z=%3d>, vel=<x=%3d, y=%3d, z=%3d>\n", moons[j].x, moons[j].y, moons[j].z, moons[j].vel_x, moons[j].vel_y, moons[j].vel_z);
    }
    while (i < repetitions) {
        ++i;
        gravity(MOON_COUNT, moons);
        velocity(MOON_COUNT, moons);
        fprintf(stdout, "After %d steps:\n", i);
        for (int j = 0; j < MOON_COUNT; ++j) {
            fprintf(stdout, "pos=<x=%3d, y=%3d, z=%3d>, vel=<x=%3d, y=%3d, z=%3d>\n",
                moons[j].x, moons[j].y, moons[j].z, moons[j].vel_x, moons[j].vel_y, moons[j].vel_z
            );
        }
        if (i % 10 == 0) {
            fprintf(stdout, "Energy after %d steps:\n", i);
            int total_energies[MOON_COUNT];
            int total_energy = 0;
            for (int j = 0; j < MOON_COUNT; ++j) {
                int pot = abs(moons[j].x)     + abs(moons[j].y)     + abs(moons[j].z);
                int kin = abs(moons[j].vel_x) + abs(moons[j].vel_y) + abs(moons[j].vel_z);
                total_energies[j] = pot * kin;
                total_energy += total_energies[j];
                fprintf(stdout, "pot: %3d + %3d + %3d = %3d;    kin: %3d + %3d + %3d = %3d;    toal: %3d * %3d = %3d\n",
                    abs(moons[j].x),     abs(moons[j].y),     abs(moons[j].z),     pot,
                    abs(moons[j].vel_x), abs(moons[j].vel_y), abs(moons[j].vel_z), kin,
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

    return 0;
}