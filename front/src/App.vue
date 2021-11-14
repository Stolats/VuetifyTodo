<template>
    <v-app>
        <v-main>
            <v-container>
                <v-row>
                    <v-col>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <new-item v-on:getItems="getItems"> </new-item>
                    </v-col>
                </v-row>
                <v-row >
                    <v-col>
                        <edit-form :todo="editTodo" :display="displayEdit" v-on:hideEditForm="hideEditForm" v-on:getItems="getItems"></edit-form>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <div value="body-1">
                            Counter: {{ counter }}
                        </div>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <v-simple-table>
                            <thead>
                                <tr>
                                    <th style="width:10%;" >Is Complete?</th>
                                    <th>Name</th>
                                    <th style="width:10%;"></th>
                                    <th style="width:10%;"></th>
                                </tr>
                            </thead>
                            <tbody >
                                <tr v-on:displayEditForm="displayEditForm" v-on:deleteItem="deleteItem" is="todo-item"
                                    v-for="item in todos"
                                    v-bind:todo="item"
                                    v-bind:key="item.id"
                                ></tr>
                            </tbody>
                        </v-simple-table>
                    </v-col>
                </v-row>
            </v-container>
        </v-main>
    </v-app>
</template>

<script>
// import test from "./components/test";
import NewItem from "./components/new-item";
import EditForm from "./components/edit-form";
import TodoItem from "./components/todo-item";

export default {
    name: "App",

    components: {
        // test,
        NewItem,
        EditForm,
        TodoItem
    },

    data: () => ({
        //
        todos: [{id:1, name:'test', isComplete: true}],
        editTodo: {id:-1, name:'test', isComplete: false},
        displayEdit: false,
        counter: 0
    }),
    methods: {
        displayEditForm: function (todo){
            // alert("edit")
            var test = JSON.parse(JSON.stringify(todo))
            this.editTodo = test
            this.displayEdit = true
        },
        hideEditForm: function() {
            // alert("hide")
            this.displayEdit = false
        },
        deleteItem: function (todo){
            var vm = this;
            // alert("delete")
            fetch(`${this.$uri}/${todo.id}`, {
                method: 'DELETE'
              })
              .then(function() {
                  vm.getItems();
              })
              .catch(error => console.error('Unable to delete item.', error));
        },
        getItems: function () {
            var vm = this;
            fetch(this.$uri)
                .then(response => response.json())
                .then(function(data) {
                vm.counter = data.length;
                vm.todos = data;
                })
                .catch(error => console.error('Unable to get items.', error));
        }
    },
    mounted: function() {
        this.getItems();
    }
};
</script>
