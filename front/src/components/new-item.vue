
<template>
    <v-form action="javascript:void(0);">
        <v-row>
            <v-col>
                <v-text-field  label="New to-do" v-model="name" v-on:keyup.enter="addItem(name)" ></v-text-field>
            </v-col>
            <v-col cols="auto">
                <v-btn 
                elevation="2"
                @click="addItem(name)"
                >Add</v-btn>
            </v-col>
        </v-row>
    </v-form>
</template>

<script>
  export default {
    name: 'NewItem',
    data: () => ({
        name: ''
    }),
    emits: ['getItems'],
    methods: {
        addItem: function (name) {
            // alert('addItem' + name);
            const item = {
                isComplete: false,
                name: name
            };
            
            fetch(this.$uri, {
                method: 'POST',
                headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
                },
                body: JSON.stringify(item)
            })
            .then(response => response.json())
            .then(() => {
                this.$emit('getItems')
                this.name = ''
                })
                .catch(error => console.error('Unable to add item.', error));
        }
    }
  }
</script>
