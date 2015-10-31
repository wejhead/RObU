# William Johnson
# Mesh Preprocessing for RObU
# Reads a .obj and returns a .ro

import numpy

def import_mesh(filename):
    vertices = numpy.array([])
    for line in open(filename, "r"):
	vals = line.split()
	if vals[0] == "v":
	    v = map(float, vals[1:])
	    vertices = numpy.append(vertices, v)
    return vertices

def eigen_vals(vector_space, n_fixed_points):
    return 

def write_file(filename, freqs, damps, amps):
    fileout = open("../output/"+filename+".ro", "w")
    fileout.write("nactive_freq:\n")
    fileout.write(str(len(freqs))+"\n")
    fileout.write("frequencies:\n")
    for freq in freqs:
	fileout.write(str(freq)+"\n")
    for damp in damps:
	fileout.write(str(damp)+"\n")
    for amp in amps:
	fileout.write(str(amp)+"\n")
    fileout.write("END\n")

def main():    

    S = import_mesh("test.obj")
    internal_friction = 1

    eigen_vals = eigendecomp(mesh, num_points) 

    freqs = gen_freqs(eigen_vals)
    damps = gen_damps(eigen_vals, internal_friction)
    amps = gen_amps(eigen_vals)

    write_file("test", freqs, damps, amps)

main()
