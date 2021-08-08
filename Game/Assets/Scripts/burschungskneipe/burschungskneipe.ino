const int BUTTON = 2; //Pushbutton Input to Pin Nr 2

int ButtonState = 0;
int lastState = 0;
int StopPressure = 0;

const int BUTTON_2 = 3; //Pushbutton Input to Pin Nr 3

int ButtonState_2 = 0;
int lastState_2 = 0;
int StopPressure_2 = 0;

void setup() {
  // put your setup code here, to run once:
  pinMode(BUTTON, INPUT_PULLUP);
  pinMode(BUTTON_2, INPUT_PULLUP);
  Serial.begin(9600);

}

void loop() {
  // put your main code here, to run repeatedly:
  ButtonState = digitalRead(BUTTON);

  if (ButtonState == LOW && lastState == LOW && StopPressure == 0) // Checking if Input from button is HIGH (1/+5V)
   {     
     //Serial.print("The button was pressed\n"); // If input is High make LED ON (HIGH/1/+5V)
     StopPressure = 1;
   }
   else if (ButtonState == HIGH && lastState == HIGH && StopPressure == 1)  
   {
      //Serial.print("The button was released\n");
      Serial.print("s\n");
      StopPressure = 0;
   }

   lastState = ButtonState;

  // put your main code here, to run repeatedly:
  ButtonState_2 = digitalRead(BUTTON_2);

  if (ButtonState_2 == LOW && lastState_2 == LOW && StopPressure_2 == 0) // Checking if Input from button is HIGH (1/+5V)
   {     
     //Serial.print("The button was pressed\n"); // If input is High make LED ON (HIGH/1/+5V)
     StopPressure_2 = 1;
   }
   else if (ButtonState_2 == HIGH && lastState_2 == HIGH && StopPressure_2 == 1)  
   {
      //Serial.print("The button was released\n");
      Serial.print("s\n");
      StopPressure_2 = 0;
   }

   lastState_2 = ButtonState_2;

}
