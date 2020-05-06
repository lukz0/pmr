<template>
    <b-jumbotron>
        <h2>Robots</h2>
        <div>
            <b-list-group horizontal="md">
                <b-list-group-item to="robots/add">Add new robot</b-list-group-item>
                <b-list-group-item to="delete">Update robot</b-list-group-item>
                <b-list-group-item to="edit">Modified</b-list-group-item>
            </b-list-group>
        </div>
    </b-jumbotron>
</template>

<script>
    export default {
        name: "Robots"
    }
</script>