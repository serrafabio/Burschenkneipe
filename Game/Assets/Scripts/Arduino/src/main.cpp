#include <Arduino.h>

const int BUTTON_1 = 2; //Pushbutton Input to Pin Nr 2
const int BUTTON_2 = 3; //Pushbutton Input to Pin Nr 3
const int BUTTON_3 = 4; //Pushbutton Input to Pin Nr 4
const int BUTTON_4 = 5; //Pushbutton Input to Pin Nr 5
const int BUTTON_5 = 6; //Pushbutton Input to Pin Nr 6
const int BUTTON_6 = 7; //Pushbutton Input to Pin Nr 7
const int BUTTON_7 = 8; //Pushbutton Input to Pin Nr 8
const int BUTTON_8 = 9; //Pushbutton Input to Pin Nr 9
const int BUTTON_9 = 10; //Pushbutton Input to Pin Nr 10
const int BUTTON_10 = 11; //Pushbutton Input to Pin Nr 11
const int BUTTON_11 = 12; //Pushbutton Input to Pin Nr 12
const int BUTTON_12 = 13; //Pushbutton Input to Pin Nr 13

// Instatiate variables
int ButtonState_1 = 0;
int lastState_1 = 0;
int StopPressure_1 = 0;

int ButtonState_2 = 0;
int lastState_2 = 0;
int StopPressure_2 = 0;

int ButtonState_3 = 0;
int lastState_3 = 0;
int StopPressure_3 = 0;

int ButtonState_4 = 0;
int lastState_4 = 0;
int StopPressure_4 = 0;

int ButtonState_5 = 0;
int lastState_5 = 0;
int StopPressure_5 = 0;

int ButtonState_6 = 0;
int lastState_6 = 0;
int StopPressure_6 = 0;

int ButtonState_7 = 0;
int lastState_7 = 0;
int StopPressure_7 = 0;

int ButtonState_8 = 0;
int lastState_8 = 0;
int StopPressure_8 = 0;

int ButtonState_9 = 0;
int lastState_9 = 0;
int StopPressure_9 = 0;

int ButtonState_10 = 0;
int lastState_10 = 0;
int StopPressure_10 = 0;

int ButtonState_11 = 0;
int lastState_11 = 0;
int StopPressure_11 = 0;

int ButtonState_12 = 0;
int lastState_12 = 0;
int StopPressure_12 = 0;


void setup() {
    // put your setup code here, to run once:
    pinMode(BUTTON_1, INPUT_PULLUP);
    pinMode(BUTTON_2, INPUT_PULLUP);
    pinMode(BUTTON_3, INPUT_PULLUP);
    pinMode(BUTTON_4, INPUT_PULLUP);
    pinMode(BUTTON_5, INPUT_PULLUP);
    pinMode(BUTTON_6, INPUT_PULLUP);
    pinMode(BUTTON_7, INPUT_PULLUP);
    pinMode(BUTTON_8, INPUT_PULLUP);
    pinMode(BUTTON_9, INPUT_PULLUP);
    pinMode(BUTTON_10, INPUT_PULLUP);
    pinMode(BUTTON_11, INPUT_PULLUP);
    pinMode(BUTTON_12, INPUT_PULLUP);
    Serial.begin(9600);

}

int buttonAction(const int button, int buttonState, int lastState, int* stopPressure)
{
    buttonState = digitalRead(button);

    if (buttonState == LOW && lastState == LOW && *stopPressure == 0) // Checking if Input from button is HIGH (1/+5V)
    {
        //Serial.print("The button was pressed\n"); // If input is High make LED ON (HIGH/1/+5V)
        *stopPressure = 1;
    } else if (buttonState == HIGH && lastState == HIGH && *stopPressure == 1) {
        //Serial.print("The button was released\n");
        Serial.print("s\n");
        *stopPressure = 0;
    }

    return buttonState;



}

void loop() {
        // put your main code here, to run repeatedly:
        lastState_1 = buttonAction(BUTTON_1, ButtonState_1, lastState_1, &StopPressure_1);
        lastState_2 = buttonAction(BUTTON_2, ButtonState_2, lastState_2, &StopPressure_2);
        lastState_3 = buttonAction(BUTTON_3, ButtonState_3, lastState_3, &StopPressure_3);
        lastState_4 = buttonAction(BUTTON_4, ButtonState_4, lastState_4, &StopPressure_4);
        lastState_5 = buttonAction(BUTTON_5, ButtonState_5, lastState_5, &StopPressure_5);
        lastState_6 = buttonAction(BUTTON_6, ButtonState_6, lastState_6, &StopPressure_6);
        lastState_7 = buttonAction(BUTTON_7, ButtonState_7, lastState_7, &StopPressure_7);
        lastState_8 = buttonAction(BUTTON_8, ButtonState_8, lastState_8, &StopPressure_8);
        lastState_9 = buttonAction(BUTTON_9, ButtonState_9, lastState_9, &StopPressure_9);
        lastState_10 = buttonAction(BUTTON_10, ButtonState_10, lastState_10, &StopPressure_10);
        lastState_11 = buttonAction(BUTTON_11, ButtonState_11, lastState_11, &StopPressure_11);
        lastState_12 = buttonAction(BUTTON_12, ButtonState_12, lastState_12, &StopPressure_12);

}
