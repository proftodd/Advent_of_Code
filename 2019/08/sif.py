import numpy as np

width = 25
height = 6

fp = open('input.txt', 'r')
line = fp.readline().strip()
fp.close()

img_array = np.array(list(map(int, line)))
img_3d = img_array.reshape((-1, height, width))

nonzeros = [(layer, np.count_nonzero(img_3d[layer, :, :])) for layer in range(img_3d.shape[0])]
max_layer = max(nonzeros, key=lambda i: i[1])
the_layer = img_3d[max_layer[0], :, :]
two_count = np.count_nonzero(the_layer == 2)
one_count = np.count_nonzero(the_layer == 1)
print(two_count * one_count)

indices = np.apply_along_axis(lambda arr: np.argmax(arr != 2), 0, img_3d)
img = np.empty((height, width), dtype='object')
for i in range(height):
    for j in range(width):
        img[i, j] = 'X' if img_3d[indices[i, j], i, j] else '.'
np.savetxt('img.txt', img, fmt='%s')
