import json
import random

def main():
    commandsFile = open('jsonFiles/bingo - Copy.json', 'r', encoding='utf-8')
    commandsStr = commandsFile.read()
    commandsFile.close()
    commands = json.loads(commandsStr)
    newCommands = {'commands':[]}

    for x in range(0, 25): #loop 25 times
        randomInt = random.randint(0, len(commands['commands']) - 1)
        newCommands['commands'].append(commands['commands'][randomInt])
        del commands['commands'][randomInt]

    #commands['commands'].append({"command": commandKey, "output": commandOutput, "mod": False})
    newCommandsFile = open('jsonFiles/generatedBingo.json', 'w', encoding='utf-8')
    json.dump(newCommands, newCommandsFile)
    newCommandsFile.close()

if __name__ == "__main__":
    main()