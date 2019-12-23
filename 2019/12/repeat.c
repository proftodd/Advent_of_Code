#include <search.h>
#include <stdio.h>
#include <stdlib.h>
#include "moon.c"

int main(int argc, char **argv)
{
    int moon_count = 4;
    struct Moon **moons = read_system(argv[1], &moon_count);

    int i = 0;
    ENTRY e, *ep;
    hcreate(10000000000);
    const char *result = to_string_system(moon_count, moons);
    e.key = (char *) result;
    e.data = (void *) (size_t) i;
    ep = hsearch(e, ENTER);
    while (1) {
        ++i;
        gravity(moon_count, moons);
        velocity(moon_count, moons);
        const char *result = to_string_system(moon_count, moons);
        e.key = (char *) result;
        ep = hsearch(e, FIND);
        if (ep) {
            break;
        } else {
            e.data = (void *) (size_t) i;
            ep = hsearch(e, ENTER);
        }
    }
    fprintf(stdout, "Repeated after %i repetitions\n", i);
    fprintf(stdout, "The earlier iteration was %d\n", (int) ep->data);
    fprintf(stdout, "%s\n", ep->key);

    for (int i = 0; i < moon_count; ++i) {
        free(moons[i]);
    }
    free(moons);

    return 0;
}