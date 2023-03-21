<template>
  <v-card max-width="448" class="mx-auto" color="grey-lighten-3">
    <v-layout>
      <v-app-bar color="purple">
        <template v-slot:image>
          <v-img
            gradient="to top right, rgba(19,84,122,.8), rgba(128,208,199,.8)"
          ></v-img>
        </template>

        <v-app-bar-title>PDI-Feather-Tracking</v-app-bar-title>

        <template v-slot:append>
          <v-btn icon="mdi-dots-vertical">User</v-btn>
        </template>
      </v-app-bar>

      <v-main>
        <slot name="main" />
      </v-main>
    </v-layout>
  </v-card>
</template>

<script>
export default {
  name: "MenuBar",
  props: {},
  computed: {
    // is_signed_in() {},
  },
  methods: {
    logout() {},
  },
};
</script>
