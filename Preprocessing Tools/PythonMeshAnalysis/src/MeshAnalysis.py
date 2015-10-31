# William Johnson
# Mesh Preprocessing for RObU
# Reads a .obj and returns a .ro

import numpy


def import_mesh(file_name):
    return 

def eigen_vals(vector_space, n_fixed_points):
    return 

def write_file(freqs, damps, amps):
    for freq in freqs:
	continue
    for damp in damps:
	continue
    for amp in amps:
	continue

def main(internal_friction):    

    S = import_mesh("test.obj")

    eigen_vals = eigendecomp(mesh, num_points) 

    freqs = gen_freqs(eigen_vals)
    damps = gen_damps(eigen_vals, internal_friction)
    amps = gen_amps(eigen_vals)

    write_file(freqs, damps, amps)

main()
