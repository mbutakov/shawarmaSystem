package main

import (
	"database/sql"
	"encoding/json"
	"fmt"
	_ "github.com/denisenkom/go-mssqldb"
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
	fmt.Fprint(c.Writer, dish)
	query := url.Values{}
	query.Add("database", "fastfood")
	u := &url.URL{
		Scheme:   "sqlserver",
		User:     url.UserPassword("fastFood", "fastFood"),
		Host:     fmt.Sprintf("%s:%d", "localhost", 1433),
		RawQuery: query.Encode(),
	}
	db, err := sql.Open("sqlserver", u.String())
	if err != nil {
		panic(err)
	}
	var datetime = time.Now()
	var o = Order{}
	o.Client = "9025927723"
	o.Employee = "9025927723"
	o.Date = datetime.Format("2006-1-2") + " " + datetime.Format("15:04:05.999")
	o.Status = "В ожиданий"
	for _, p := range dish {
		fmt.Println("Вы заказали:"+p.Name+" Цена: ", p.Price)
	}
	id := 0
	err = db.QueryRow(fmt.Sprintf("INSERT INTO [Order](Client,Employee,[Date],[Status]) OUTPUT Inserted.ID VALUES ('%s','%s','','%s')", o.Client, o.Employee, o.Status)).Scan(&id)
	if err != nil {
		panic(err)
	}
	fmt.Fprint(c.Writer, dish)
	sqlStr := "INSERT INTO [OrderCompound] ([Order],[Dish],[Price],[Count],[Status]) OUTPUT Inserted.[ORDER] VALUES "
	vals := []interface{}{}
	for _, row := range dish {
		sqlStr += "('%v', '%v', '%.2f', '%v', '%s'),"
		vals = append(vals, id, row.Id, row.Price, row.Count, o.Status)
	}
	sqlStr = sqlStr[0 : len(sqlStr)-1]
	stmt, _ := db.Prepare(sqlStr)
	sqlStr = fmt.Sprintf(sqlStr, vals...)
	fmt.Println(sqlStr)
	fmt.Println(stmt)
	idTwo := 0
	err = db.QueryRow(sqlStr).Scan(&idTwo)
	if err != nil {
		panic(err)
	}

	fmt.Println(idTwo)
	defer db.Close()
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
	Id    uint16  `json:"Id"`
	Name  string  `json:"Name"`
	Price float32 `json:"Price"`
	Count uint16  `json:"Count"`
}
type Order struct {
	Id       uint16
	Client   string
	Employee string
	Date     string
	Status   string
}
