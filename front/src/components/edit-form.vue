
<template>
        <v-row>
            <v-col>
                <v-form id="editForm" v-if="display">
                    <v-row>
                        <v-col cols="auto">
                            <h3>Edit</h3>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col v-if="false">
                            <v-text-field   v-model="todo.id"></v-text-field>
                        </v-col>
                        <v-col cols="auto">
                            <v-checkbox v-model="todo.isComplete"></v-checkbox>
                        </v-col>
                        <v-col>
                            <v-text-field  label="" v-model="todo.name"></v-text-field>
                        </v-col>
                        <v-col cols="auto">
                            <v-btn
                                elevation="2"
                                @click="updateItem(todo)"
                            >Save</v-btn>
                        </v-col>
                        <v-col cols="auto">
                            <v-icon
                                elevation="2"
                                fab
                                @click="$emit('hideEditForm', todo)"
                            >mdi-close</v-icon>
                        </v-col>
                    </v-row>
                </v-form>
            </v-col>
        </v-row>
</template>

<script>
  export default {
    name: 'EditForm',
    // data: () => ({
    //     display: true
    // }),
    props: ['todo', 'display'],
    emits: ['hideEditForm', 'getItems'],
    methods: {
        updateItem: function(todo) {
            var vm = this;
            fetch(`${this.$uri}/${todo.id}`, {
                method: 'PUT',
                headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
                },
                body: JSON.stringify(todo)
            })
            .then(function() {
                vm.$emit('getItems')
            })
            .catch(error => console.error('Unable to update item.', error));

            this.$emit('hideEditForm', todo)

            return false;
        }
    }
  }
</script>
