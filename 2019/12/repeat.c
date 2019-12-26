#include <search.h>
#include <stdio.h>
#include <stdlib.h>
#include "moon.c"

typedef long long int ll;

static void copy_moons(const int moon_count, struct Moon **src, struct Moon **dest)
{
    for (int i = 0; i < moon_count; ++i) {
        for (int j = 0; j < DIMENSIONS; ++j) {
            dest[i]->coord[j] = src[i]->coord[j];
            dest[i]->velocity[j] = src[i]->velocity[j];
        }
    }
}

// Algorithm for determination of GCD:
// https://www.geeksforgeeks.org/lcm-of-given-array-elements/
int gcd(int a, int b)
{
    if (b == 0) {
        return a;
    }
    return gcd(b, a % b);
}

ll findlcm(int arr[], int n)
{
    ll ans = arr[0];
    for (int i = 1; i < n; ++i) {
        ans = (((arr[i] * ans)) / (gcd(arr[i], ans)));
    }
    return ans;
}

int main(int argc, char **argv)
{
    int moon_count = 0;
    struct Moon **orig_moons = read_system(argv[1], &moon_count);
    struct Moon **moons = malloc(sizeof(struct Moon) * moon_count);
    for (int i = 0; i < moon_count; ++i) {
        moons[i] = malloc(sizeof(struct Moon));
    }

    ENTRY e, *ep;
    int axis_repeat[DIMENSIONS];
    for (int axis = 0; axis < DIMENSIONS; ++axis) {
        copy_moons(moon_count, orig_moons, moons);
        int i = 0;
        hcreate(10000000000);
        const char *result = to_vector_matrix(moon_count, moons, axis);
        e.key = (char *) result;
        e.data = (void *) (size_t) i;
        ep = hsearch(e, ENTER);
        while (1) {
            ++i;
            gravity(moon_count, moons);
            velocity(moon_count, moons);
            const char *result = to_vector_matrix(moon_count, moons, axis);
            e.key = (char *) result;
            ep = hsearch(e, FIND);
            if (ep) {
                break;
            } else {
                e.data = (void *) (size_t) i;
                ep = hsearch(e, ENTER);
            }
            if (i % 10000 == 0) {
                fprintf(stdout, "No match found for %d after %d iterations\n", axis, i);
            }
        }
        axis_repeat[axis] = i;
        hdestroy();
    }
    fprintf(stdout, "Repeated after (%d,%d,%d) repetitions\n", axis_repeat[0], axis_repeat[1], axis_repeat[2]);
    fprintf(stdout, "The whole system repeats in %lld iterations\n", findlcm(axis_repeat, DIMENSIONS));

    for (int j = 0; j < moon_count; ++j) {
        free(orig_moons[j]);
        free(moons[j]);
    }
    free(orig_moons);
    free(moons);

    return 0;
}