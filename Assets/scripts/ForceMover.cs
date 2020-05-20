using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CommunicationNamespace;
public class ForceMover : MonoBehaviour
{
    public Text speedInfoUI;
    public int identify;
    public float speed;
    public Rigidbody ball;
    public float mass;
    public string type;
    private CommunicationItem toSend;
    void Start() // инициализация объекта
    {
        ball = GetComponent<Rigidbody>();
        toSend.mass = mass;
        toSend.speed = speed;
        toSend.setType(type);
        setupInfoUI(speed);
       

    }

    public void getParamsOfCollision(CommunicationItem item) // системная функция выбирающая тип рассчета скорочти в зависимости от типа столковения отправленного столкнувшимся объектом
    {
        if (item.getType() == "r")
        {
            Debug.Log("It is resilent");
            resilientCollision(item);
            return;
            
        }
        nonResilientCollision(item);
        
    }
    private void OnTriggerEnter(Collider other) // функция которая посылает объекту с которым он столкнулся данные о собственной скорости, массе и типе столкновения
    {
        Debug.Log("Collision start!");
        
        other.gameObject.SendMessage("getParamsOfCollision", toSend);
    }

    public void setModeOfCollision() // функция также не относящиеся к физике
    {
        if (type == "r")
        {
            type = "nr";
            toSend.setType("nr");
        }
        else
        {
            type = "r";
            toSend.setType("r");
        }
    }

    void Update()
    {
    }

    void resilientCollision(CommunicationItem item) // функция считающая значение скорочти при упругом столкновении
    {
        Debug.Log("- - - - - -  - - - -  - - -");
        Debug.Log(item.getMass());
        Debug.Log(item.getSpeed());
        Debug.Log(toSend.getMass());
        Debug.Log(toSend.getSpeed());
        Debug.Log("- - - - - -  - - - -  - - -");
        float newSpeed = (2 * item.getMass() * item.getSpeed() + (toSend.getMass() - item.getMass())*toSend.getSpeed())/(item.getMass() + toSend.getMass()); // рассчет скорости по формуле
        Debug.Log(newSpeed);
        ball.velocity = new Vector3(newSpeed, 0, 0); // присваивание объекту значение новой скорочти
        setupInfoUI(newSpeed);
    }

    void nonResilientCollision(CommunicationItem item) // функция считающая значение скорочти объекта при неупругом столкновении
    {
        float newSpeed = (item.getMass() * item.getSpeed() + toSend.getMass() * toSend.getSpeed()) /
                         (toSend.getMass() + item.getMass()); // соответственно рассчет скорочти по формуле 
        ball.velocity = new Vector3(newSpeed, 0,0); // присваивание объекту значение новой скорочти
        setupInfoUI(newSpeed);
    }

    void setupData(CommunicationSetup item) // системная функция не относящиеся к физике
    {
        if (item.isMass)
        {
            this.mass = item.value;
            this.toSend.mass = item.value;
            return;
        }

        this.speed = item.value;
        this.toSend.speed = item.value;
        Debug.Log("Setup !!" + item.value.ToString());
    }

    public void reset(float x)
    {
        ball.position = new Vector3(x, 0, 0); // телепортирует объекты на стартовые позиции
        setupInfoUI(0); 
    }

    private void setupInfoUI(float speedInfo) // обновляет значение скорости объекта в текстовом поле снизу
    {
        if (identify == 0)
        {
            speedInfoUI.text = "left speed = " + speedInfo.ToString();
        }
        else
        {
            speedInfoUI.text = "right speed = " + speedInfo.ToString();
        }
    }

    private void start() // функция которая задает скорость объекту при нажатии на кнопку старт
    {
        ball.velocity = new Vector3(speed, 0, 0); // эта строка задает скорость объекту, полученную из текстового поля
        setupInfoUI(speed); 
    }

    private void resetButton() // функция которая ставит на стартовую позицию объект
    {
        setupInfoUI(0);
        ball.velocity = new Vector3(0, 0, 0);
        if (identify == 0)
        {
            ball.position = new Vector3(0, 0, 0);
        }
        else
        {
            ball.position = new Vector3(5, 0,0);
        }
    }
}


