# Stalagmite
A CLI made to experiment with C#, and also because i wanted to make my own cli

# Command list

- anything between plain <> is mandatory  
- wildcards are specified after ; of the argument  
- arguments that start with ? means their optional  
- .. indicates any number of arguments  
- if a paramater has limited values their going to be specified after : of the argument   
- sometimes parameters are only mandatory if a paramter has a specific value, it's going to be indicated between ! in this case  

```
New directory, syntax :               nwd <dirname>
Remove directory, syntax :            rmd <dirname;*>
Delete file, syntax :                 dlf <filename;*>
Wipe directory, syntax:               wipe <dirname>
Change directory, syntax :            cd <dirname>
List, syntax :                        ls <?dirname>
Enviroment Variable, syntax :         var <varname> <value>
Execute Program, syntax :             exec <program> <?..>
Net Request, syntax :                 netr <method:get|post> <url> <!method=post!jsonfile>
```

# Upcoming
- I'm Planning to add a GUI so that's it doesn't need a terminal to run (it's gonna have it's own)  
- Support of more HTTP methods  
- Use the env and variables, currently you can declare vars but that's it  
- Add scripting  
- Add procedures (mini-scripts that can be loaded directly like functions)
