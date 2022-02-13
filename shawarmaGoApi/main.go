package main

import (
	"database/sql"
	"encoding/json"
	"fmt"
	_ "github.com/denisenkom/go-mssqldb"
	mssql "github.com/denisenkom/go-mssqldb"
	"github.com/gin-gonic/gin"
	"net/url"
	"time"
)

var router *gin.Engine

func main() {

	router = gin.Default()
	router.Static("/assets/", "front/")
	router.LoadHTMLGlob("templates/*.html")
	router.GET("/getList", getListDishesToWork)
	router.POST("/acceptOrder", acceptOrder)
	_ = router.Run(":8080")

}

func acceptOrder(c *gin.Context) {
	dish := []Dish{}
	if err := c.BindJSON(&dish); err != nil {
		return
	}

	query := url.Values{}
	query.Add("database", "fastfood")
	u := &url.URL{
		Scheme: "sqlserver",
		User:   url.UserPassword("fastFood", "fastFood"),
		Host:   fmt.Sprintf("%s:%d", "localhost", 1433),
		// Path:  instance, // if connecting to an instance instead of a port
		RawQuery: query.Encode(),
	}
	db, err := sql.Open("sqlserver", u.String())
	if err != nil {
		panic(err)
	}
	defer db.Close()
	var datetime = time.Now()
	dt := datetime.Format(time.RFC3339)
	var o = Order{}
	o.Client = "9025927723"
	o.Employee = "9025927723"
	o.Date = dt
	o.Status = "В ожиданий"
	for _, p := range dish {
		fmt.Println("Вы заказали:"+p.Name+" Цена: ", p.Price)
	}
	//_, err = db.Exec("INSERT into Order2(Name) VALUES ('')")
	fmt.Fprint(c.Writer, dish)

}

func getListDishesToWork(c *gin.Context) {
	query := url.Values{}
	query.Add("database", "fastfood")

	u := &url.URL{
		Scheme: "sqlserver",
		User:   url.UserPassword("fastFood", "fastFood"),
		Host:   fmt.Sprintf("%s:%d", "localhost", 1433),
		// Path:  instance, // if connecting to an instance instead of a port
		RawQuery: query.Encode(),
	}
	db, err := sql.Open("sqlserver", u.String())
	if err != nil {
		panic(err.Error())
	}
	rows, err := db.Query("select * from Dish")
	if err != nil {
		panic(err)
	}
	defer rows.Close()
	dish := []Dish{}
	for rows.Next() {
		p := Dish{}
		err := rows.Scan(&p.Id, &p.Name, &p.Price)
		if err != nil {
			fmt.Println(err)
			continue
		}
		dish = append(dish, p)

	}
	b, err := json.Marshal(dish)
	c.Writer.Write(b)
	c.HTML(200, "index.html", gin.H{})

}

type Dish struct {
	Id    uint16        `json:"Id"`
	Name  mssql.VarChar `json:"Name"`
	Price float32       `json:"Price"`
}
type Order struct {
	Id       uint16
	Client   string
	Employee string
	Date     string
	Status   string
}
