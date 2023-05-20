# I'm broke okay? Don't judge me
# Not like I have any other way to keep my pc running while I'm away.
# Wow I'm VERY sad
# ...Anyway this moves the mouse to a random location so my bot can live.

import pyautogui
from time import sleep
from random import randint
from typing import Final

if __name__ == "__main__":
    pyautogui.FAILSAFE = True
    print("Initialised.")
    
    DURATION: Final[int] = 1
    while True:
        x: int = randint(500, 1000)
        y: int = randint(500, 1000)
        pyautogui.moveTo(x, y, DURATION)

        print(f"Mouse moved to {x}, {y}")

        sleep(5*60)  # 10 Minutes
