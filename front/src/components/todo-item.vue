
<template>
        <tr>
            <td><v-checkbox disabled v-model="todo.isComplete"></v-checkbox></td>
            <td>{{todo.name}}</td>
            <td>
                <v-btn
                    elevation="2"
                    @click="$emit('displayEditForm', todo)"
                    >Edit
                </v-btn>
            </td>
            <td>
                <v-btn
                    elevation="2"
                    @click="$emit('deleteItem', todo)"
                    >Delete
                </v-btn>
            </td>
        </tr>  
</template>

<script>
  export default {
    name: 'TodoItem',
    props: ['todo'],
    emits: ['displayEditForm', 'deleteItem'],
  }
</script>
