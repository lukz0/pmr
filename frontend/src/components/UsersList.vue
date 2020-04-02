<template>
    <b-card-group deck>
        <b-card header="Users in database">
            <b-list-group>
                <b-list-group-item v-for="user in users" :key="user.id"  class="d-flex justify-content-between align-items-center">
                    {{user.firstName + ' ' + user.lastName}}
                    <b-badge
                            @click="$emit('del-user', user.id)"
                            variant="danger" pill>
                        Delete user
                        <span v-if="user.deleting"><em> - Deleting...</em></span>
                        <span v-else-if="user.deleteError" class="text-danger"> - ERROR: {{user.deleteError}}</span>
                    </b-badge>
                </b-list-group-item>
            </b-list-group>
        </b-card>
    </b-card-group>
</template>

<script>
    export default {
        name: "UsersList",
        props: ['users']
    }
</script>