# William Johnson
# Mesh Preprocessing for RObU
# Reads a .obj and returns a .ro

import numpy

def import_mesh(filename):
    vertices = numpy.array([0, 0, 0], ndmin=2)
    for line in open(filename, "r"):
	vals = line.split()
	if vals[0] == "v":
	    v = map(float, vals[1:])
	    v = numpy.array(v, ndmin=2)
            vertices = numpy.concatenate((vertices, v), axis=0)
    return vertices[1:]

def get_eigen_vals(mass_matrix, stiffness_matrix):
    return numpy.linalg.eig(vector_space) 

def get_freqs():
    return

def get_damps():
    return

def get_amps():
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

    m = import_mesh("test.obj")     #mass matrix
    k = numpy.ones_like(m)          #stiffness matrix
    internal_friction = 1           #internal friction 

    eigen_vals = get_eigen_vals(m, k) 

    freqs = get_freqs(eigen_vals)
    damps = get_damps(eigen_vals, internal_friction)
    amps = get_amps(eigen_vals)

    write_file("test", freqs, damps, amps)

