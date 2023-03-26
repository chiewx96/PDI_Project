<template>
  <v-content>
    <v-container fluid fill-height>
      <v-layout align-center justify-center>
        <div class="justify-center min-width" size="md">
          <v-card-text>
            <v-form lazy-validation @submit.prevent="login">
              <v-text-field
                v-model="input.username"
                prepend-icon="mdi-account"
                name="login"
                label="Login"
                type="text"
                autocomplete="new-username"
              ></v-text-field>
              <v-text-field
                v-model="input.password"
                id="password"
                prepend-icon="mdi-lock"
                name="password"
                label="Password"
                type="password"
                autocomplete="new-password"
              ></v-text-field>
              <v-btn color="primary" type="submit">Login</v-btn>
            </v-form>
          </v-card-text>
        </div>
      </v-layout>
    </v-container>
  </v-content>
</template>

<script>
import ApiService from "@/services/api.service";
import { mapMutations } from "vuex";

export default {
  name: "LoginView",
  data() {
    return {
      input: {
        username: "",
        password: "",
      },
    };
  },
  methods: {
    ...mapMutations(["setUser", "setToken"]),
    login() {
      if (this.input.username != "" && this.input.password != "") {
        // This should actually be an api call not a check against this.$parent.mockAccount
        ApiService._post("user/login", {
          username: this.input.username,
          password: this.input.password,
        }).then(async (response) => {
          if (response.status == 200) {
            const { status, message } = await response.json();
            if (status == true) {
              this.setUser(message.user);
              this.setToken(message.token);
              this.$router.push("/scan");
            }
          }
        });
      } else {
        console.log("A username and password must be present");
      }
    },
  },
};
</script>

<style>
.min-width {
  min-width: 300px;
  margin: auto;
  margin-top: 150px;
}
</style>
