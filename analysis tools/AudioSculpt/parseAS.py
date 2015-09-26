#!/usr/bin/env python

# william johnson sept 25 2015
# parses AudioSculpt partial analysis into .ro file 
# for real time modal synthesis in unity

# AS partial analysis must be exported with duration as the header and then
# freq and amplitude as the rows, nothing else

def main():
    #define file paths 
    inPath = raw_input("path of file to be parsed (with extension): ")
    outPath = raw_input("path of output file (no extension): ")+".ro"
    
    #decare fda array
    fda= []
    
    #open the input file
    fI = open(inPath, "r")
    f = 0
    a = 0
    c = 0
    d = 0
    firstline = True
    for line in fI:    
        if (line != "\n"):
            l = line.rstrip("\n")
            l = line.split("\t")
            if(firstline):
                d = 1/float(l[0])
                firstline = False    
            else: 
                f+= float(l[0])
                a+= float(l[1])
                c+= 1
        elif(line == "\n" and c != 0) :
            f = f/c
            a = a/c
            fda.append([f, d, a])
            c = 0
        else:
            f = 0
            a = 0
            d = 0
            firstline = True
            
    #print fda
    
    #open the output file 
    fO = open(outPath, "w")
    
    fO.write("nactive_freq:\n")
    fO.write(str(len(fda))+"\n")
    fO.write("frequencies:\n")
    for i in fda:
        fO.write(str(i[0])+"\n")
    fO.write("dampings:\n")
    for i in fda:
        fO.write(str(i[1])+"\n")
    fO.write("amplitudes[point][freq]:\n")
    for i in fda:
        fO.write(str(i[2])+"\n")
    fO.write("END\n")

main()


        
    