var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

void Button1_Clicked(object sender, EventArgs e) {
    ScriptManager.RegisterStartupScript(this, this.GetType(), "but", "buclicked()", true);
}

