#include <string.h>

struct Moon {
    int x, y, z;
    int vel_x, vel_y, vel_z;
};

struct Moon **read_system(const char *filename, int *number)
{
    const char *format = "<x=%d, y=%d, z=%d>\n";
    FILE *file = fopen(filename, "r");
    char current_line[100];
    int count = 0;
    while (fgets(current_line, sizeof(current_line), file) != NULL) {
        ++count;
    }
    fclose(file);
    *number = count;

    file = fopen(filename, "r");
    struct Moon **moons = malloc(sizeof(struct Moon *) * count);
    count = 0;
    int x, y, z;
    while (fgets(current_line, sizeof(current_line), file) != NULL) {
        sscanf(current_line, format, &x, &y, &z);
        struct Moon *new_moon = malloc(sizeof(struct Moon));
        new_moon->x = x;
        new_moon->y = y;
        new_moon->z = z;
        new_moon->vel_x = 0;
        new_moon->vel_y = 0;
        new_moon->vel_z = 0;
        moons[count] = new_moon;
        ++count;
    }
    fclose(file);
    return moons;
}

static char *to_string(struct Moon *moon)
{
    char *result;
    const char *format = "pos=<x=%3d, y=%3d, z=%3d>, vel=<x=%3d, y=%3d, z=%3d>";
    asprintf(&result, format, moon->x, moon->y, moon->z, moon->vel_x, moon->vel_y, moon->vel_z);
    return result;
}

char *to_string_system(int moon_count, struct Moon *moons[])
{
    char *result, *old_result;
    char *this_moon = to_string(moons[0]);
    asprintf(&result, "%s", this_moon);
    free(this_moon);
    for (int i = 1; i < moon_count; ++i) {
        old_result = result;
        char *this_moon = to_string(moons[i]);
        asprintf(&result, "%s\n%s", old_result, this_moon);
        free(this_moon);
        free(old_result);
    }
    return result;
}

void gravity(int moon_count, struct Moon *moons[])
{
    for (int i = 0; i < moon_count; ++i) {
        for (int j = i + 1; j < moon_count; ++j) {
            if (moons[i]->x < moons[j]->x) {
                ++moons[i]->vel_x;
                --moons[j]->vel_x;
            } else if (moons[i]->x > moons[j]->x) {
                --moons[i]->vel_x;
                ++moons[j]->vel_x;
            }
            if (moons[i]->y < moons[j]->y) {
                ++moons[i]->vel_y;
                --moons[j]->vel_y;
            } else if (moons[i]->y > moons[j]->y) {
                --moons[i]->vel_y;
                ++moons[j]->vel_y;
            }
            if (moons[i]->z < moons[j]->z) {
                ++moons[i]->vel_z;
                --moons[j]->vel_z;
            } else if (moons[i]->z > moons[j]->z) {
                --moons[i]->vel_z;
                ++moons[j]->vel_z;
            }
        }
    }
}

void velocity(int moon_count, struct Moon *moons[])
{
    for (int i = 0; i < moon_count; ++i) {
        moons[i]->x += moons[i]->vel_x;
        moons[i]->y += moons[i]->vel_y;
        moons[i]->z += moons[i]->vel_z;
    }
}
